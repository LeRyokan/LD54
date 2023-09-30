using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class BatController : MonoBehaviour
{
    private BatControls m_batControls;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private Vector3 WingFlapDir;
    [SerializeField] private float WingFlapForce;
    [SerializeField] private Vector2 WingDir;

    [SerializeField][Range(-64f, 64f)] private float panning;
    private FMOD.Studio.EventInstance instance;
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        DetectSideOfCollision(collision);
    }

    public void DetectSideOfCollision(Collision2D collision)
    {
        // collision tag must be one of (wall, ennemy or safe space)
        string[] colliders = { "TilemapWall", "Ennemy", "SafeSpace" };
        panning = 0;
        instance.setParameterByName("Pan (Wall Sonar Bounce)", panning);
        if(colliders.Any(collision.gameObject.name.Contains))
        {
            Vector2 hit = collision.contacts[0].normal;
            Debug.Log(hit);
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/Char/Bat/Sonar");
            // hit.x different from 0 means bat touched a wall on left or right
            if(hit.x != 0)
            {
                var distanceX = collision.transform.position.x - transform.position.x;
                Debug.Log(distanceX);
                if (hit.x < 0f)
                {
                    Debug.Log("Pan to Right");
                    panning = distanceX * 10;
                } else if (distanceX > 0f)
                {
                    Debug.Log("Pan to Left");
                    panning = distanceX * 10;
                }
            }
            instance.setParameterByName("Pan (Wall Sonar Bounce)", panning);
            instance.start();
            // reset value so if next collision is up/bottom only the sound is centered
        }
    }
}
