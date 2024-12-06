
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour
{
    public PlayerStatData psd;

    public float dmg = 6;
    public GameObject target;
    public Transform arrow;
    public float arrowDistance = 1.5f;
    // public float rotationOffset = 90f;

    private Vector3 lastPosition;
    private Vector3 originalScale;  // Store the original scale

    // PSD stats (these stats persist over runs, see PSD functions below for explanation)
    public int maxhp = 5;
    public int currenthp = 5;
    private int minhp = 0;
    public float invincibilityTime = 0.5f; // in s
    public float bleedChance;
    public float critChance;

    // After taking a hit, player should be invincible for a bit
    public GameObject shieldSprite;

    public bool invincible = false;

    // To do with soul drops
    public float soulDropMult = 1f;

    // hehe death sound
    public AudioSource deathsound;

    void Start()
    {
        // Overwrite player PSD with PSD saved in GameManager
        LoadFromGameManager();

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
    
    // PSD things //

    // * these functions handle the player stats being duplicated over scenes so you can meta progress
    private void LoadFromGameManager()
    {
        if (GameManager.Instance != null && GameManager.Instance.psd != null)
        {
            Debug.LogWarning("PSD is found, loading from PSD...");
            psd = GameManager.Instance.psd;
            SetStatsFromPSD();
        }
        else
        {
            Debug.LogWarning("GameManager or PlayerStatsData is missing!");
        }
    }
    private void SaveToGameManager()
    {
        // PROBABLY DONT NEED THIS UNLESS YOU'RE META-PROGRESSING INGAME
    }
    private void SetStatsFromPSD()
    {
        maxhp = psd.maxhp;
        currenthp = psd.currenthp;
        bleedChance = psd.bleedChance;
        critChance = psd.critChance;
        invincibilityTime = psd.invincibilityTime;
        gameObject.GetComponent<PlayerMove>().moveSpeed = psd.speed;

        Debug.Log("Player stats successfully loaded from psd!");
    }
    void HandleArrowRotation()
    {
        // Get the mouse position in world space (adjust z to match camera)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        // Ensure mousePosition.z is set to a fixed value (same as the player or the intended 2D depth)
        mousePosition.z = transform.position.z; // Make sure the arrow stays on the same z plane as the player

        // Calculate the direction vector from the player to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calculate arrow's new position (keeping a fixed distance from the player)
        arrow.position = transform.position + direction * arrowDistance;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation with the offset
        arrow.rotation = Quaternion.Euler(0f, 0f, angle);
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
        if (!invincible)
        {
            Debug.Log("AH DAMAGE");
            currenthp -= damage;
            if (currenthp <= minhp)
            {
                currenthp = 0;
                deathsound.Play();
                StartCoroutine(DelayedDeath(0.8f));
            }
            else
            {
                //Debug.Log("Starting invincibility frames");
                invincible = true; // Set invincible immediately
                shieldSprite.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(iFrames());
            }
        }
    }
    protected virtual void Die()
    {
        // death logic goes here
        Debug.Log("Death has occured!");
        SceneManager.LoadScene("NewUpgradeMenu");
    }
    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        Die();
    }
    private IEnumerator iFrames()
    {
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false; // Reset invincibility
        shieldSprite.GetComponent<SpriteRenderer>().enabled = false;
        //Debug.Log("Invincibility ended");
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