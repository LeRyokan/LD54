using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarWave : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private float m_sonarMaxDistance; 
    [SerializeField] private float m_currentSonarDistance;
    [SerializeField] private Vector3 m_initialDirection;
    [SerializeField] private bool m_isInCooldown;
    [SerializeField] private float m_shootCooldown;
    [SerializeField] private float m_sonarForce;

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
        }
    }
    
    private void ReflectProjectile(Rigidbody2D rb, Vector3 reflectVector)
    {    
        Debug.Log($"NORMAL OF THE WALL IS : {reflectVector}");
        m_initialDirection = Vector3.Reflect(m_initialDirection, reflectVector);
        rb.velocity = m_initialDirection;
    }
}
