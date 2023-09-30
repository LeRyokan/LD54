using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SonarWave : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private float m_sonarMaxDistance; 
    [SerializeField] private float m_currentSonarDistance;
    [SerializeField] private Vector3 m_initialDirection;
    [SerializeField] private bool m_isInCooldown;
    [SerializeField] private float m_shootCooldown;
    [SerializeField] private float m_sonarForce;
    [SerializeField][Range(-64f, 64f)] private float panning;
    private FMOD.Studio.EventInstance instance;

    private Vector3 m_velocity;

    private void OnEnable()
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootSonar(Vector3 pos, Vector3 dir)
    {
        //TP le sonar sur la chauve souris
        transform.position = pos;
        m_initialDirection = dir;
        m_rigidbody2D.AddForce(m_initialDirection * m_sonarForce,ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Wall"))
        {
            ReflectProjectile(m_rigidbody2D, col.contacts[0].normal);
            playSoundOnSonarCollision(col, "event:/Char/Bat/Sonar"); // should be event:/Environnement/Wall/SonarDetect
        }
    }
    
    private void ReflectProjectile(Rigidbody2D rb, Vector3 reflectVector)
    {    
        Debug.Log($"NORMAL OF THE WALL IS : {reflectVector}");
        m_initialDirection = Vector3.Reflect(m_initialDirection, reflectVector);
        rb.velocity = m_initialDirection;
    }

    public void playSoundOnSonarCollision(Collision2D col, string collidedObjectEvent)
    {
        // collision tag must be one of (wall, ennemy or safe space)
        string[] colliders = { "TilemapWall", "Ennemy", "SafeSpace" };
        // reset value so if next collision is up/bottom only the sound is centered
        panning = 0;
        instance.setParameterByName("Pan (Wall Sonar Bounce)", panning);
        if(colliders.Any(col.gameObject.name.Contains))
        {
            Vector2 hit = col.contacts[0].normal;
            instance = FMODUnity.RuntimeManager.CreateInstance(collidedObjectEvent);
            // hit.x different from 0 means bat touched a wall on left or right
            if(hit.x != 0)
            {
                var distanceX = col.transform.position.x - transform.position.x;
                if (hit.x < 0f)
                {
                    // Right panning
                    panning = distanceX * 10;
                } else if (distanceX > 0f)
                {
                    // Left panning
                    panning = distanceX * 10;
                }
            }
            instance.setParameterByName("Pan (Wall Sonar Bounce)", panning);
            instance.start();
        }
    }
}
