using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public PlayerStatData psd;

    public float dmg = 5;
    public float atkspeed = 100f;     // uses the inverse i.e. 1/x so increasing this does increase atk speed

    public float relicDamageMult = 1f;
    public float relicAtkspeedMult = 1f;

    public GameObject target;
    public Transform weapon;
    public float weaponDistance = 1f;

    private Vector3 originalScale;  // Store the original scale

    public int maxhp = 5;
    public int currenthp = 5;
    private int minhp = 0;
    public float invincibilityTime = 0.5f;
    public float bleedChance;
    public float critChance;

    public GameObject shieldSprite;
    public bool invincible = false;

    public float soulDropMult = 1f;
    public AudioSource deathsound;

    void Start()
    {
        LoadFromGameManager();
        Cursor.lockState = CursorLockMode.Confined;
        originalScale = transform.localScale;
    }

    void Update()
    {
        RotateWeaponTowardCursor();
        HandlePlayerFlipToCursor();
        HandleAttack();
    }
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
    void HandlePlayerFlipToCursor()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        mousePosition.z = transform.position.z;

        // Determine if the mouse is to the left or right of the player
        bool isMouseToLeft = mousePosition.x < transform.position.x;

        // Flip the player based on the mouse position
        if (isMouseToLeft)
        {
            // Flip player to the left
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            // Flip player to the right
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        // Flip weapon 180 degrees when the player faces left (based on the player's facing direction)
        if (isMouseToLeft)
        {
            weapon.localRotation = Quaternion.Euler(0f, 180f, 0f); // Flip weapon when facing left
        }
        else
        {
            weapon.localRotation = Quaternion.Euler(0f, 0f, 0f); // Reset weapon rotation when facing right
        }

        // Now rotate the weapon toward the cursor, regardless of facing direction
        RotateWeaponTowardCursor();
    }
    void RotateWeaponTowardCursor()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        mousePosition.z = transform.position.z; // Ensure it's on the same Z-plane as the player and weapon

        // Get direction from weapon to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Get the angle in degrees for the weapon's rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the weapon
        weapon.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    // THE DAMAGE FORMULA
    public int GetActualDamage(float damageMultiplier)
    {
        return Mathf.RoundToInt(dmg * damageMultiplier * relicDamageMult);
    }
    public float GetAtkSpeed(float atkspeedMultiplier)
    {
        return Mathf.RoundToInt(atkspeed * atkspeedMultiplier * relicAtkspeedMult);
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
                invincible = true;
                shieldSprite.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(iFrames());
            }
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Death has occurred!");
        SceneManager.LoadScene("NewUpgradeMenu");
    }
    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Die();
    }
    private IEnumerator iFrames()
    {
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
        shieldSprite.GetComponent<SpriteRenderer>().enabled = false;
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
        bleedChance += bleedChanceIncrease;
    }
}