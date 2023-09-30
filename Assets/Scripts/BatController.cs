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
    [SerializeField] private bool m_isDead;
    [SerializeField] private SonarWave m_sonarWave;
    [SerializeField] private Animator m_animator;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    
    [SerializeField] private float velocity;
   
    [SerializeField] public bool isInSafeSpace;
    [SerializeField] public SafeSpace currentSafeSpace;

    private Vector2 m_mousePosition;
    private FMOD.Studio.EventInstance flapSoundInstance;
    private FMOD.Studio.EventInstance hitWallSoundInstance;
    private FMOD.Studio.EventInstance finishScreamInstance;
    private FMOD.Studio.EventInstance sonarScreamInstance;
    
    private void Awake()
    {
        
        
    }
    private void OnEnable()
    {
        m_batControls = new BatControls();
        m_batControls.Enable();
                
        //Init animations
        m_animator.SetBool("CanSonar",true);
        
        m_batControls.Gameplay.WingFlap.performed += WingFlapOnperformed;
        m_batControls.Gameplay.Sonar.performed += SonarOnperformed;
        m_batControls.Gameplay.LeaveSafeSpace.performed += LeaveSafeSpaceOnperformed;
        
        //always spawn in a safe space
        //ActivateSafeSpace();
    }

    private void LeaveSafeSpaceOnperformed(InputAction.CallbackContext obj)
    {
        if (isInSafeSpace)
        {
            DisableSafeSpace();
        }
    }

    private void OnDisable()
    {
        m_batControls.Gameplay.WingFlap.performed -= WingFlapOnperformed;
        m_batControls.Gameplay.Sonar.performed -= SonarOnperformed;
        m_batControls.Gameplay.LeaveSafeSpace.performed -= LeaveSafeSpaceOnperformed;
    }
    
    private void SonarOnperformed(InputAction.CallbackContext obj)
    {
        if (isInSafeSpace) 
            return;
        
        var mousePositionInWorld = mainCamera.ScreenToWorldPoint(m_mousePosition);
            
        //Lui donner le vecteur de direction par rapport a la position de la souris 
        var computeDir = new Vector3(mousePositionInWorld.x - transform.position.x,mousePositionInWorld.y - transform.position.y,0);
        if (!m_sonarWave.m_isInCooldown)
        {
            m_animator.SetBool("CanSonar",false);
            m_animator.SetTrigger("Sonar");
            StartCoroutine(m_sonarWave.ShootSonarAndFade(transform.position, computeDir.normalized));
            StartCoroutine(CooldownSonarAnim());
            sonarScreamInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Char/Bat/Scream");
            sonarScreamInstance.start();
        }
    }

    private void WingFlapOnperformed(InputAction.CallbackContext obj)
    {
        if (isInSafeSpace) 
            return;
        
        var moveDir = new Vector3(0, m_wingFlapDir.y, 0); // flap only move upward using force
        m_rigidbody.AddForce(moveDir * m_wingFlapForce,ForceMode2D.Impulse);
        m_animator.SetTrigger("Flap");
        flapSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Char/Bat/Flap");
        flapSoundInstance.start();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_wingDir = m_batControls.Gameplay.Direction.ReadValue<Vector2>();
        if(m_wingDir.x < 0)
        {
            m_spriteRenderer.flipX = true;
        }
        else
        {
            m_spriteRenderer.flipX = false;
        }
        
        m_mousePosition = m_batControls.Gameplay.MousePosition.ReadValue<Vector2>();
        var originPos = m_rigidbody.transform.position;
        var inputPos = new Vector3(m_wingDir.x * velocity, 0, 0);
        m_rigidbody.transform.position = originPos + inputPos;
    }

    public IEnumerator CooldownSonarAnim()
    {
        m_rigidbody.gravityScale = 0f;
        m_rigidbody.velocity = Vector2.zero;
        m_batControls.Disable();
        
        yield return new WaitForSeconds(1f);
        
        m_batControls.Enable();
        m_rigidbody.gravityScale = 1f;
        
        yield return new WaitForSeconds(1f);
        m_animator.SetBool("CanSonar",true);
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Wall"))
        {
            hitWallSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Char/Bat/Hit");
            hitWallSoundInstance.start();
        } else if (col.transform.CompareTag("DeathWall")) {
            Death();
            Debug.Log("Player is dead");
        } 
    }

    public void ActivateSafeSpace()
    {
        m_animator.SetBool("IsSafe",true);
        isInSafeSpace = true;
        m_rigidbody.velocity = Vector2.zero;
        m_rigidbody.gravityScale = 0f;
        m_batControls.Gameplay.Sonar.Disable();
        m_batControls.Gameplay.WingFlap.Disable();
        m_batControls.Gameplay.Direction.Disable();
    }

    public void DisableSafeSpace()
    {
        m_animator.SetBool("IsSafe",false);
        isInSafeSpace = false;
        m_rigidbody.gravityScale = 1f;
        m_batControls.Gameplay.Sonar.Enable();
        m_batControls.Gameplay.WingFlap.Enable();
        m_batControls.Gameplay.Direction.Enable();
    }
    
    public void Death() {
        // TODO
        m_isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("FINISH");
            finishScreamInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Finish");
            finishScreamInstance.start();
        }
    }
}
