using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BatController : MonoBehaviour
{
    private BatControls m_batControls;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private Vector3 WingFlapDir;
    [SerializeField] private float WingFlapForce;
    [SerializeField] private Vector2 WingDir;
    private void Awake()
    {
        m_batControls = new BatControls();
        m_batControls.Enable();
    }

    private void OnEnable()
    {
        m_batControls.Gameplay.WingFlap.performed += WingFlapOnperformed;
        m_batControls.Gameplay.Sonar.performed += SonarOnperformed;
       // m_batControls.Gameplay.Direction.performed += DirectionOnperformed;
        
    }
    private void OnDisable()
    {
        m_batControls.Gameplay.WingFlap.performed -= WingFlapOnperformed;
        m_batControls.Gameplay.Sonar.performed -= SonarOnperformed;
        //m_batControls.Gameplay.Direction.performed -= DirectionOnperformed;
    }

    private void DirectionOnperformed(InputAction.CallbackContext obj)
    {
        WingDir = obj.ReadValue<Vector2>();
    }
    
    private void SonarOnperformed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    private void WingFlapOnperformed(InputAction.CallbackContext obj)
    {
        Debug.Log("FLAP FLAP");
        var moveDir = new Vector3(WingDir.x, WingFlapDir.y, 0);
        
        m_rigidbody.AddForce(moveDir * WingFlapForce,ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WingDir = m_batControls.Gameplay.Direction.ReadValue<Vector2>();
    }
}
