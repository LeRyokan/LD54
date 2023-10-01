using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using Unity.Collections;

public class SonarWave : MonoBehaviour
{
    public bool m_isInCooldown;
    private Vector3 m_initialDirection;
    private Vector3 m_velocity;
    [SerializeField] private BatController m_bat;
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private Transform m_graphics;
    [SerializeField] private GameObject m_maskArea;

    [SerializeField] private float m_revealTime;
    
    [Header("Tweaking value")]
    [SerializeField] private float m_shootCooldown = 2;
    [SerializeField] private float m_sonarForce;
    [SerializeField] private float m_sonarFinalScale;

    [Header("Sound design")]
    [SerializeField][Range(-64f, 64f)] private float panning;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance detectSafeSpaceInstance;
    private FMOD.Studio.EventInstance RevealInOutInstance;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Wall"))
        {
            RevealWallOnHit(col.contacts[0].point);
            ReflectProjectile(m_rigidbody2D, col.contacts[0].normal);
            playSoundOnSonarCollision(col, "event:/Char/Bat/Sonar Reveals Wall"); // should be event:/Environnement/Wall/SonarDetect
        }
    }
    
    private void ReflectProjectile(Rigidbody2D rb, Vector3 reflectVector)
    {    
        m_initialDirection = Vector3.Reflect(m_initialDirection, reflectVector);
        m_graphics.up = m_initialDirection;
        // rb.velocity = m_initialDirection;
    }

    public void RevealWallOnHit(Vector2 pos)
    {
        var currentMask = Instantiate(m_maskArea, pos, Quaternion.identity);
        
        currentMask.transform.localScale = new Vector3(1,1,1);
        currentMask.SetActive(true);
        RevealInOutInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Environnement/RevealInOut");
        RevealInOutInstance.start();
        currentMask.transform.DOScale(m_sonarFinalScale, m_revealTime/2).OnComplete(() => 
            currentMask.transform.DOScale(0, m_revealTime).OnComplete(() => 
                DestroyMask(currentMask)
            )
        );
    }

    public void DestroyMask(GameObject obj)
    {
        obj.SetActive(false);
        Destroy(obj);
    }
    
    public void playSoundOnSonarCollision(Collision2D col, string collidedObjectEvent)
    {
        // collision tag must be one of (wall, ennemy or safe space)
        // string[] colliders = { "TilemapWall", "Ennemy", "SafeSpace" };
        // reset value so if next collision is up/bottom only the sound is centered
        panning = 0;
        instance.setParameterByName("Pan (Wall Sonar Bounce)", panning);
        if(col.transform.CompareTag("Wall") || col.transform.CompareTag("DeathWall"))
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
                    panning = distanceX * 1000;
                } else if (distanceX > 0f)
                {
                    // Left panning
                    panning = distanceX * 1000;
                }
            }
            instance.setParameterByName("Pan (Wall Sonar Bounce)", panning);
            var distanceBetweenBatAndCollider = Vector3.Distance(m_bat.transform.position, transform.position);
            instance.start();
        } else if (col.transform.CompareTag("SafeZone")) 
        {
            detectSafeSpaceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Environnement/Safe Space/SonarDetect");
            detectSafeSpaceInstance.start();
        }
    }

    public IEnumerator ShootSonarAndFade(Vector3 pos, Vector3 dir)
    {
        if (m_isInCooldown)
        {
            yield return null;
        } else 
        {
            m_isInCooldown = true;
            m_rigidbody2D.gameObject.SetActive(true);

            //TP le sonar sur la chauve souris
            transform.position = pos;
            m_initialDirection = dir;
            m_rigidbody2D.velocity = Vector2.zero;
            m_rigidbody2D.AddForce(m_initialDirection * m_sonarForce,ForceMode2D.Impulse);
            m_graphics.up = m_initialDirection;
            yield return new WaitForSeconds(m_shootCooldown);

            m_rigidbody2D.gameObject.SetActive(false);
            m_isInCooldown = false;
        }
    }
}
