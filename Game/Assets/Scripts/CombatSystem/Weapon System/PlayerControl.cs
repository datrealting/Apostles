using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour
{
    public float dmg = 6;
    public GameObject target;
    public Transform arrow;
    public float arrowDistance = 1.5f;
    public float rotationOffset = 90f;

    private Vector3 lastPosition;
    private Vector3 originalScale;  // Store the original scale
    // HP / Hearts
    public int maxhp = 5;
    public int currenthp = 5;
    private int minhp = 0;
    public float bleedChance;
    public float critChance;

    // After taking a hit, player should be invincible for a bit
    public float invincibilityTime = 1000; // in ms
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        lastPosition = transform.position;
        originalScale = transform.localScale;  // Save the original scale
    }

    void Update()
    {
        HandleArrowRotation();
        HandlePlayerFlip();
        HandleAttack();
    }

    void HandleArrowRotation()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Set z to 0 for 2D

        // Calculate the direction from the player to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Set the arrow's position at a fixed distance from the player
        arrow.position = transform.position + direction * arrowDistance;

        // Calculate the angle from the player to the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the arrow
        arrow.rotation = Quaternion.Euler(0, 0, angle - rotationOffset);

        // Ensure the arrow stays on top of the player sprite
        arrow.position = new Vector3(arrow.position.x, arrow.position.y, transform.position.z - 1);
    }

    void HandlePlayerFlip()
    {
        Vector3 currentPosition = transform.position;

        // Check if moving left or right
        if (currentPosition.x > lastPosition.x)
        {
            // Moving right, ensure the scale is positive
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (currentPosition.x < lastPosition.x)
        {
            // Moving left, flip only the x-axis
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        lastPosition = currentPosition;  // Update last position
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            try
            {
                target.GetComponent<NPCStats>().TakeDamage(dmg);
                Debug.Log(target.GetComponent<NPCStats>().currenthp + " / " + target.GetComponent<NPCStats>().maxhp);
            }
            catch
            {
                Debug.Log("Target is invalid");
            }
        }
    }
    public virtual void TakeDamage(int damage)
    {
        currenthp -= damage;
        if (currenthp <= minhp)
        {
            currenthp = 0;
            Die();
        }
    }
    protected virtual void Die()
    {
        // death logic goes here
        Debug.Log("Death has occured!");
        SceneManager.LoadScene("UpgradeMenu");
    }

    public virtual void Heal(int healAmount)
    {
        currenthp += healAmount;
        if (currenthp > maxhp)
        {
            currenthp = maxhp;
        }
    }


    public virtual void bleedChanceIncrease(float bleedChanceIncrease)
    {
        bleedChance = bleedChance + bleedChanceIncrease;
    }
}