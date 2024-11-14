using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Variables

    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;


    // Update is called once per frame
    void Update()
    {
        Inputs();
    } // Update based on FPS (variable)

    void FixedUpdate () 
    {
        Move();
    } // Update based on physics (constant)

    void Inputs ()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2 (moveX, moveY).normalized;
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
