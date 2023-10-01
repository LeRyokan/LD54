using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance { get; private set; }
    public FMOD.Studio.EventInstance backgroundMusicInstance;
    private FMOD.Studio.EventInstance randomStressInstance;
    private FMOD.Studio.EventInstance ambienceInstance;
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
        backgroundMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Levels");
        backgroundMusicInstance.start();
        randomStressInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Environnement/SFX/RandomStress");
        randomStressInstance.start();
        ambienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Environnement/Ambience");
        ambienceInstance.start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
