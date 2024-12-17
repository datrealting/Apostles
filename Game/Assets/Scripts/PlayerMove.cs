using UnityEngine;
using System.Collections;
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

    private bool isIdle = false; // Tracks if the character is in Idle state
    private Coroutine blinkCoroutine; // Reference to the blinking coroutine

    // Dash variables
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private float lastDashTime = -1f;

    // Movement acceleration/deceleration variables
    public float moveAcceleration = 5f; // Acceleration rate
    public float moveDeceleration = 5f; // Deceleration rate
    private float currentMoveSpeed = 0f; // Current speed of movement

    void Update()
    {
        Inputs();
        HandleBlinking();
        HandleDashInput();
    }

    void FixedUpdate()
    {
        if (!isDashing) // Prevent movement during dash
        {
            SmoothMovement();
        }
    }

    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        float moveMagnitude = Mathf.Sqrt(moveX * moveX + moveY * moveY);
        animator.SetFloat("Speed", moveMagnitude);

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void SmoothMovement()
    {
        // Handle acceleration if moving
        if (moveDirection.magnitude > 0)
        {
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, moveSpeed, moveAcceleration * Time.fixedDeltaTime);
        }
        // Handle deceleration if idle
        else
        {
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, 0, moveDeceleration * Time.fixedDeltaTime);
        }

        rb.linearVelocity = moveDirection * currentMoveSpeed;
    }

    // Dash logic
    void HandleDashInput()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= lastDashTime + dashCooldown && !isDashing) // Right click
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        Debug.Log("Dash started!");

        isDashing = true;
        lastDashTime = Time.time;

        // Get the cursor position in world space
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the cursor
        Vector2 dashDirection = (cursorPosition - (Vector2)transform.position).normalized;

        if (dashDirection == Vector2.zero) // If no cursor movement, dash forward
        {
            dashDirection = Vector2.up;
        }

        float currentDashSpeed = 0f; // Start with no speed
        float accelerationTime = dashDuration * 0.5f; // Half of the dash duration for acceleration
        float decelerationTime = dashDuration * 0.5f; // The other half for deceleration
        float dashEndTime = Time.time + dashDuration;

        // Accelerate towards the dash speed
        while (Time.time < dashEndTime - decelerationTime)
        {
            currentDashSpeed = Mathf.Lerp(0f, dashSpeed, (Time.time - lastDashTime) / accelerationTime);
            rb.linearVelocity = dashDirection * currentDashSpeed;

            yield return null;
        }

        // Decelerate towards zero
        while (Time.time < dashEndTime)
        {
            currentDashSpeed = Mathf.Lerp(dashSpeed, 0f, (Time.time - (dashEndTime - decelerationTime)) / decelerationTime);
            rb.linearVelocity = dashDirection * currentDashSpeed;

            yield return null;
        }

        rb.linearVelocity = Vector2.zero; // Ensure to stop movement after dash
        isDashing = false;

        Debug.Log("Dash ended!");
    }

    // Handle idle state and blinking
    void HandleBlinking()
    {
        float speed = animator.GetFloat("Speed");
        if (speed < 0.01f)
        {
            if (!isIdle)
            {
                isIdle = true;
                blinkCoroutine = StartCoroutine(BlinkRoutine());
            }
        }
        else
        {
            if (isIdle && blinkCoroutine != null)
            {
                isIdle = false;
                StopCoroutine(blinkCoroutine);
            }
        }
    }

    IEnumerator BlinkRoutine()
    {
        while (isIdle)
        {
            float randomDelay = Random.Range(6f, 10f);
            yield return new WaitForSeconds(randomDelay);

            animator.SetFloat("BlinkTime", 1f); // Start blink animation
            yield return null; // Play for one frame
            animator.SetFloat("BlinkTime", 0f); // Reset
        }
    }

    // Movement speed adjustment methods
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
        moveSpeed = (baseMoveSpeed + moveSpeedAdd + relicMoveSpeedAdd) * relicMoveSpeedMult * moveSpeedMult;
        if (moveSpeed < 0)
        {
            moveSpeed = 0;
        }
    }
}
