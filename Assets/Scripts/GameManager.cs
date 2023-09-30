using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private BatController m_playerBat;
    [SerializeField] private List<StartZone> m_levelStartZoneList;
    private int m_currentLevel = 0;
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
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
    }

    private void LoadEndGameScreen()
    {
        Debug.Log("END GAME GG");
    }
}
