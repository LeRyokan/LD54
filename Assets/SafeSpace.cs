using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SafeSpace : MonoBehaviour
{
    public bool isActive = true;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    public void DestroySafeSpace()
    {
        isActive = false;
        m_spriteRenderer.DOFade(0f, 2f).OnComplete(() => Destroy(gameObject));
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("DESTROY");
            DestroySafeSpace();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ENTER");
            Vector3 newPos = new Vector3(0, 0 ,0);
            other.GetComponent<BatController>().ActivateSafeSpace(transform.position);
        }
    }
    
}
