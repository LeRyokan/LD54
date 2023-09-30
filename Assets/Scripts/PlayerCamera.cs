using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private FMOD.Studio.EventInstance backgroundMusicInstance;
    private float levelNumber = 0;
    // m_currentLevel = 0 for first level, 1 for 2nd level, and on so on
 
    // Start is called before the first frame update
    void Start()
    {
        levelNumber = GameManager.Instance.m_currentLevel;
        backgroundMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Levels");
        backgroundMusicInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        levelNumber = GameManager.Instance.m_currentLevel;
        backgroundMusicInstance.setParameterByName("Playing Track", levelNumber);
    }
}
