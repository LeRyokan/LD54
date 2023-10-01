using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private BatControls m_batControls;
    public static GameManager Instance { get; private set; }
    [SerializeField] private BatController m_playerBat;
    [SerializeField] private List<LevelInfo> m_levelStartZoneList;
    [SerializeField] private List<GameObject> m_instanciatedLevels;
    [SerializeField] private CanvasGroup m_canvasGroupIntro;
    [SerializeField] private CanvasGroup m_canvasGroupEnd;
    [SerializeField] private CanvasGroup m_canvasGroupLoose;
    [SerializeField] private CanvasGroup m_canvasGroupInGame;
    private UI_State m_currentState;
    public int m_currentLevel = 0;
    
    public enum UI_State
    {
         Start,
         Dead,
         End,
         InGame
    }
    
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance FinishSoundInstance;
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        DOTween.Init();
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
        } 
        else 
        { 
            Instance = this; 
        }

        //m_instanciatedLevels = new List<GameObject>();

    }

    private void OnEnable()
    {
        m_currentState = UI_State.Start;
        m_batControls = new BatControls();
        m_batControls.Enable();
        m_batControls.Gameplay.Disable();
        m_batControls.Menu.Enable();
        m_batControls.Menu.Start.performed += StartOnperformed;
    }

    private void OnDisable()
    {
        m_batControls.Menu.Start.performed -= StartOnperformed;
    }

    private void StartOnperformed(InputAction.CallbackContext obj)
    {
        m_batControls.Gameplay.Enable();
        m_batControls.Menu.Disable();
        
        switch (m_currentState)
        {
            case UI_State.Start:
                m_canvasGroupIntro.interactable = false;
                m_canvasGroupIntro.blocksRaycasts = false;
                m_canvasGroupIntro.DOFade(0f, 1f);
                m_canvasGroupInGame.DOFade(1f, 1f);
                break;
            case UI_State.Dead:
                m_canvasGroupLoose.interactable = false;
                m_canvasGroupLoose.blocksRaycasts = false;
                m_canvasGroupLoose.DOFade(0f, 1f);
                LoadLevelAndSetPlayerSpawn();
                break;
            case UI_State.End:
                m_canvasGroupEnd.interactable = false;
                m_canvasGroupEnd.blocksRaycasts = false;
                m_canvasGroupEnd.DOFade(0f, 1f);
                Application.Quit();
                break;
        }

        m_currentState = UI_State.InGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadLevelAndSetPlayerSpawn();
    }

    public void LoadNextLevels()
    {
        FinishSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Finish");
        FinishSoundInstance.start();
        m_currentLevel++;
        PlayerCamera.Instance.backgroundMusicInstance.setParameterByName("Music Track", m_currentLevel);
        if (m_currentLevel < m_levelStartZoneList.Count)
        {
            LoadLevelAndSetPlayerSpawn();
        }
        else
        {
            LoadEndGameScreen();
        }
    }
    
    //DEPRECATED
    /*public void LoadNextLevel()
    {
        m_currentLevel++;
        PlayerCamera.Instance.backgroundMusicInstance.setParameterByName("Music Track", m_currentLevel);
        if (m_currentLevel < m_levelStartZoneList.Count)
        {
            Instantiate(m_levelStartZoneList[m_currentLevel].gameObject,new Vector3(0,0,0),quaternion.identity);
            MoveBatToSpawn(m_currentLevel);
        }
        else
        {
            LoadEndGameScreen();
        }
    }*/

    //DEPRECATED
    /*private void MoveBatToSpawn(int id)
    {
        var nextPos = m_levelStartZoneList[id].playerSpawn;
        m_playerBat.transform.position = nextPos.transform.position;
        m_playerBat.ActivateSafeSpace();
    }*/

    private void LoadEndGameScreen()
    {
        m_playerBat.FinishGame();
        m_currentState = UI_State.End;
        m_batControls.Gameplay.Disable();
        m_batControls.Menu.Enable();
        m_canvasGroupInGame.DOFade(0f, 1f);
        m_canvasGroupEnd.interactable = true;
        m_canvasGroupEnd.blocksRaycasts = true;
        m_canvasGroupEnd.DOFade(1f, 1f);
    }

    public void ShowDeadScreen()
    {
        m_currentState = UI_State.Dead;
        m_batControls.Gameplay.Disable();
        m_batControls.Menu.Enable();
        m_canvasGroupInGame.DOFade(0f, 1f);
        m_canvasGroupLoose.interactable = true;
        m_canvasGroupLoose.blocksRaycasts = true;
        m_canvasGroupLoose.DOFade(1f, 1f);
    }

    private void LoadLevelAndSetPlayerSpawn()
    {
        var currentLevel = Instantiate(m_levelStartZoneList[m_currentLevel].gameObject,new Vector3(0,0,0),quaternion.identity);
        m_instanciatedLevels.Add(currentLevel);
        var nextPos = currentLevel.GetComponent<LevelInfo>().playerSpawn;
        m_playerBat.transform.position = nextPos.transform.position;
        m_playerBat.ActivateSafeSpace();
        
        //Destroy previous level
        if (m_currentLevel >= 1)
        {
            var previouslevel = m_currentLevel - 1;
            m_instanciatedLevels[previouslevel].SetActive(false);
        }
    }
}