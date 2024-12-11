using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Variables

    public Animator animator;

    public float baseMoveSpeed = 3f;
    public float moveSpeed;

    public float moveSpeedAdd = 0f;  
    public float moveSpeedMult = 1f;
    public float relicMoveSpeedAdd = 0f;
    public float relicMoveSpeedMult = 1f;

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

        float moveSpeed = Mathf.Sqrt(moveX * moveX + moveY * moveY);

        animator.SetFloat("Speed", moveSpeed);

        moveDirection = new Vector2 (moveX, moveY).normalized;
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    // Call every time you want to adjust movement speed. Feed in the buff/debuff as flat percentage
    public void AddAddSpeed(float speed)
    {
        moveSpeedAdd += speed;
        AdjustMoveSpeed();
    }
    public void AddMultSpeed(float speed)
    {
        moveSpeedMult += speed;
        AdjustMoveSpeed();
    }
    public void AddRelicAddSpeed(float speed)
    {
        relicMoveSpeedAdd += speed; 
        AdjustMoveSpeed();
    }
    public void AddRelicMultSpeed(float speed)
    {
        relicMoveSpeedMult += speed;
        AdjustMoveSpeed();
    }
    public void AdjustMoveSpeed()
    {
        /*
        Debug.Log("Move Speed = " + moveSpeed);
        Debug.Log("Base Move Speed = " + baseMoveSpeed);
        Debug.Log("Move Speed Add = " + moveSpeedAdd);
        Debug.Log("Move Speed Mult = " + moveSpeedMult);
        Debug.Log("Relic Move Speed Add = " + relicMoveSpeedAdd);
        Debug.Log("Relic Move Speed Mult = " + relicMoveSpeedMult);
        */
        moveSpeed = (baseMoveSpeed + moveSpeedAdd + relicMoveSpeedAdd) * relicMoveSpeedMult * moveSpeedMult;
        Debug.Log("Move Speed = " + moveSpeed);
        if (moveSpeed < 0)
        {
            moveSpeed = 0;
        }
    }
}
