using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private FMOD.Studio.EventInstance backgroundMusicInstance;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Levels");
        backgroundMusicInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
