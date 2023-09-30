using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class BatController : MonoBehaviour
{
    private BatControls m_batControls;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private Vector3 m_wingFlapDir;
    [SerializeField] private float m_wingFlapForce;
    [SerializeField] private Vector2 m_wingDir;
    [SerializeField] private Vector2 m_mousePosition;
    [SerializeField] private SonarWave m_sonarWave;
    
    private void Awake()
    {
        m_batControls = new BatControls();
        m_batControls.Enable();
    }
    private void OnEnable()
    {
        m_batControls.Gameplay.WingFlap.performed += WingFlapOnperformed;
        m_batControls.Gameplay.Sonar.performed += SonarOnperformed;
    }
    private void OnDisable()
    {
        m_batControls.Gameplay.WingFlap.performed -= WingFlapOnperformed;
        m_batControls.Gameplay.Sonar.performed -= SonarOnperformed;
    }
    
    private void SonarOnperformed(InputAction.CallbackContext obj)
    {
        var mousePositionInWorld = mainCamera.ScreenToWorldPoint(m_mousePosition);
        Debug.Log($"Sonar Send at pos : {mousePositionInWorld}" );
        
        //Lui donner le vecteur de direction par rapport a la position de la souris 
        var computeDir = new Vector3(mousePositionInWorld.x - transform.position.x,mousePositionInWorld.y - transform.position.y,0);
        Debug.Log($"Sonar direction : {computeDir}" );
        
        m_sonarWave.ShootSonar(transform.position,computeDir.normalized);
    }

    private void WingFlapOnperformed(InputAction.CallbackContext obj)
    {
        Debug.Log("FLAP FLAP");
        var moveDir = new Vector3(m_wingDir.x, m_wingFlapDir.y, 0);
        m_rigidbody.AddForce(moveDir * m_wingFlapForce,ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_wingDir = m_batControls.Gameplay.Direction.ReadValue<Vector2>();
        m_mousePosition = m_batControls.Gameplay.MousePosition.ReadValue<Vector2>();
    }
}