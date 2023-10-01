using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BatController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private Animator m_animator;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Slider m_slider;
    [SerializeField] private SonarWave m_sonarWave;
    
    private BatControls m_batControls;
    private Vector2 m_wingDir;
    private bool m_isDead;
    private bool m_finishGame;
    private float m_staminaMax = 100f;
    private float m_currentStamina;
    public bool isInSafeSpace;
    
    [Header("Tweaking value")]
    [SerializeField] private Vector3 m_wingFlapDir;
    [SerializeField] private float m_wingFlapForce;
    [SerializeField] private float velocity;
   [SerializeField] private int m_staminaPerFlap;

   [SerializeField] private int m_reflectForceOnWallCollideX;

    private Vector2 m_mousePosition;
    private FMOD.Studio.EventInstance flapSoundInstance;
    private FMOD.Studio.EventInstance hitWallSoundInstance;
    private FMOD.Studio.EventInstance finishScreamInstance;
    private FMOD.Studio.EventInstance sonarScreamInstance;
    
    private void Awake()
    {
        m_currentStamina = m_staminaMax;
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
        }

    private void OnDisable()
    {
        m_batControls.Gameplay.WingFlap.performed -= WingFlapOnperformed;
        m_batControls.Gameplay.Sonar.performed -= SonarOnperformed;
        m_batControls.Gameplay.LeaveSafeSpace.performed -= LeaveSafeSpaceOnperformed;
    }

    private void LeaveSafeSpaceOnperformed(InputAction.CallbackContext obj)
    {
        if (isInSafeSpace)
        {
            DisableSafeSpace();
        }
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
        if (isInSafeSpace || m_currentStamina <= 0) 
            return;
        
        m_currentStamina -= m_staminaPerFlap;
        UpdateStaminaBar();
        
        
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
        m_mousePosition = m_batControls.Gameplay.MousePosition.ReadValue<Vector2>();
        m_wingDir = m_batControls.Gameplay.Direction.ReadValue<Vector2>();
        
        if(m_wingDir.x < 0)
        {
            m_spriteRenderer.flipX = true;
        }
        else
        {
            m_spriteRenderer.flipX = false;
        }
        
        var originPos = m_rigidbody.transform.position;
        var inputPos = new Vector3(m_wingDir.x * velocity, 0, 0);
        m_rigidbody.transform.position = originPos + inputPos;

        if (isInSafeSpace)
        {
            m_currentStamina += 0.2f;
            if (m_currentStamina > m_staminaMax)
            {
                m_currentStamina = m_staminaMax;
            }

            UpdateStaminaBar();
        }
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
            // colliding with a wall deactivate input controller for a short time
            // and pushes you away from the wall
            StartCoroutine(freezeBatAndPushBatOffTheWall(col));
            hitWallSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Char/Bat/Hit");
            hitWallSoundInstance.start();
        } else if (col.transform.CompareTag("DeathWall")) {
            Death();
            Debug.Log("Player is dead");
        }
    }

    public IEnumerator freezeBatAndPushBatOffTheWall(Collision2D col)
    {

        m_spriteRenderer.DOFade(0f, 0.1f).SetLoops(5, LoopType.Yoyo).OnComplete(() => m_spriteRenderer.DOFade(1f,0.2f));
        
        m_batControls.Disable(); // disable controls
        var reflectX = Vector3.zero;
        // as bat hitbox is circle and wall is square weird stuff happens (value can be < 0 / > 0 instead of 1 / -1)
        if (col.contacts[0].normal.x > 0)
        {
            reflectX = new Vector3(m_reflectForceOnWallCollideX, 0, 0);
        } else if (col.contacts[0].normal.x < 0)
        {
            reflectX = new Vector3(-m_reflectForceOnWallCollideX, 0, 0);
        }
        // add inverse force of normal vector from wall collision
        m_rigidbody.AddForce(reflectX, ForceMode2D.Impulse); // push bat off the wall
        yield return new WaitForSeconds(0.5f);
        // after 0.5 second we reset the velocity (remove previous force)
        m_rigidbody.velocity = Vector2.zero;
        // and then let the user use inputs back again !
        m_batControls.Enable();
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
    
    public void Death()
    {
        if (m_finishGame)
            return;
        
        GameManager.Instance.ShowDeadScreen();
        m_isDead = true;
    }

    public void FinishGame()
        {
            m_finishGame = true;
        }

    public void UpdateStaminaBar()
    {
        m_slider.value = m_currentStamina / 100f;
    }
}
