using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody m_rigidbody;

    [SerializeField] private int m_speedCoef; 

    private float timer = 0f;
    private Vector2 movement;

    private Vector3 movementVec3;

    private void FixedUpdate()
    {
        m_rigidbody.MovePosition(m_rigidbody.position + movementVec3 * m_speedCoef * Time.fixedDeltaTime);
    }

    public void Move(Vector2 input)
    {
        Debug.Log("HELLO");
        movement = input;
        movementVec3 = new Vector3(movement.x, 0, movement.y);
    }

    public void Jump()
    {
        Debug.Log("JUMP");
        m_rigidbody.AddForce(new Vector3(0,10,0),ForceMode.Impulse);
    }
}
