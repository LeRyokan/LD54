using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private BatControls m_batControls;
    public static GameManager Instance { get; private set; }
    [SerializeField] private BatController m_playerBat;
    [SerializeField] private List<StartZone> m_levelStartZoneList;
    [SerializeField] private CanvasGroup m_canvasGroupIntro;
    [SerializeField] private CanvasGroup m_canvasGroupEnd;
    [SerializeField] private CanvasGroup m_canvasGroupLoose;
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
                break;
            case UI_State.Dead:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                m_canvasGroupLoose.interactable = false;
                m_canvasGroupLoose.blocksRaycasts = false;
                m_canvasGroupLoose.DOFade(0f, 1f);
                break;
            case UI_State.End:
                m_canvasGroupEnd.interactable = false;
                m_canvasGroupEnd.blocksRaycasts = false;
                m_canvasGroupEnd.DOFade(0f, 1f);
                break;
        }

        m_currentState = UI_State.InGame;

    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        MoveBatToSpawn(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        m_currentLevel++;
        PlayerCamera.Instance.backgroundMusicInstance.setParameterByName("Music Track", m_currentLevel);
        if (m_currentLevel < m_levelStartZoneList.Count)
        {
            MoveBatToSpawn(m_currentLevel);
}
        else
        {
            LoadEndGameScreen();
        }
    }

    private void MoveBatToSpawn(int id)
    {
        var nextPos = m_levelStartZoneList[id];
        m_playerBat.transform.position = nextPos.transform.position;
        m_playerBat.ActivateSafeSpace();
    }

    private void LoadEndGameScreen()
    {
        m_playerBat.FinishGame();
        m_currentState = UI_State.End;
        m_batControls.Gameplay.Disable();
        m_batControls.Menu.Enable();
        m_canvasGroupEnd.interactable = true;
        m_canvasGroupEnd.blocksRaycasts = true;
        m_canvasGroupEnd.DOFade(1f, 1f);
    }

    public void ShowDeadScreen()
    {
        m_currentState = UI_State.Dead;
        m_batControls.Gameplay.Disable();
        m_batControls.Menu.Enable();
        m_canvasGroupLoose.interactable = true;
        m_canvasGroupLoose.blocksRaycasts = true;
        m_canvasGroupLoose.DOFade(1f, 1f);
    }
}
