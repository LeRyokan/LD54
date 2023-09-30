using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZone : MonoBehaviour
{

    [SerializeField] private BatController playerBat;

    private void Awake()
    {
        playerBat.transform.position = transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
