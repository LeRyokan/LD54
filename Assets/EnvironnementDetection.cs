using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironnementDetection : MonoBehaviour
{
    private FMOD.Studio.EventInstance collideSoundInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void onTriggerEnter2D(Collider2D other) {
    //     // if (other.transform.compareTag("Wall"))
    //     // {
    //     //     return null;
    //     // } else if (other.transform.compareTag("Enemy"))
    //     // {
    //     //     return null;
    //     // } else 
    //     if (other.transform.CompareTag("Finish"))
    //     {
    //         collideSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Char/Bat/Win");
    //         collideSoundInstance.start();
    //     }
    // }
}
