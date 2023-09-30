using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private BatControls m_batControls;
    public static GameManager Instance { get; private set; }
    [SerializeField] private BatController m_playerBat;
    [SerializeField] private List<StartZone> m_levelStartZoneList;
    [SerializeField] private CanvasGroup m_canvasGroup;
    public int m_currentLevel = 0;
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        DOTween.Init();
        
        m_batControls = new BatControls();
        m_batControls.Enable();
        m_batControls.Gameplay.Disable();
        m_batControls.Menu.Enable();
        m_batControls.Menu.Start.performed += StartOnperformed;
        
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void StartOnperformed(InputAction.CallbackContext obj)
    {
        m_batControls.Gameplay.Enable();
        m_batControls.Menu.Disable();
        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = false;
        m_canvasGroup.DOFade(0f, 1f);

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
        Debug.Log("END GAME GG");
    }
}
