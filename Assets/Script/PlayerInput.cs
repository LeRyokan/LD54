using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private Player m_player;
    private PlayerControls m_playerControls;

    private void Awake()
    {
        m_playerControls = new PlayerControls();
        m_playerControls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnEnable()
    {
        m_playerControls.Ground.Move.performed += MoveOnPerformed;
        m_playerControls.Ground.Jump.performed += JumpOnPerformed;
    }
    
    private void OnDisable()
    {
        m_playerControls.Ground.Move.performed -= MoveOnPerformed;
        m_playerControls.Ground.Jump.performed -= JumpOnPerformed;
    }
    
   
    
    private void MoveOnPerformed(InputAction.CallbackContext obj)
    {
        var dir = obj.ReadValue<Vector2>();
        m_player.Move(dir);
    }
    
    private void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        m_player.Jump();
    }
}
