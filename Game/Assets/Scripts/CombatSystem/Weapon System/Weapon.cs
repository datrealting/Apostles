using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string Name;

    [SerializeField] protected WeaponStats weaponStats;

    public abstract void Attack();
    protected PlayerControl playerControlReference;
    private bool canAttack = true;
    protected Transform playerTransform; // Reference to the player
    private Transform weaponPosition; // Reference to the weapon holder
    private Vector3 localOffset; // Local offset to keep the weapon attached to the player



    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerControlReference = GameObject.Find("Player").GetComponent<PlayerControl>();
        weaponPosition = GameObject.FindGameObjectWithTag("weaponHolder").transform;

        // Calculate the initial offset from the player's position
        localOffset = transform.localPosition;
    }

    protected virtual void Update()
    {
        RotateWeaponTowardCursor(); // Rotate the weapon toward the cursor
    }

    public void LevelUp()
    {
        // Increase the weapon level
        weaponStats.LevelUp();
    }


    public void HandleAttackInput()
    {

        if (playerControlReference == null)
        {
            Debug.LogError("PlayerControl reference is not set.");
            return;
        }
        // Check if the player clicks the left mouse button
        if (canAttack)
        {
            if (Input.GetMouseButton(0)) // Left mouse button
            {
                Debug.Log(1 / (playerControlReference.GetAtkSpeed(weaponStats.atkSpeed)));
                Attack(); // Trigger the attack
                canAttack = false;
                StartCoroutine(WeaponCD());
            }
        }
    }
    private IEnumerator WeaponCD()
    {
        yield return new WaitForSeconds(1 / (playerControlReference.GetAtkSpeed(weaponStats.atkSpeed)));
        canAttack = true;
    }



    protected void HandleLevelUpInput()
    {
        //
#if UNITY_EDITOR
        // Check if the player is pressing the L key
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Leveling up weapon");
            LevelUp();
        }
#endif
    }


    protected virtual bool RotateWeaponTowardCursor()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        mousePosition.z = transform.position.z; // Make sure it stays in the same z-plane (not invisible lol)

        // Calculate direction from weapon to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Check if the player is flipped (facing left)
        if (playerTransform.localScale.x < 0)
        {
            // Adjust the angle for the flipped orientation
            angle += 180f; // Mirror the weapon's rotation when flipped
        }

        // Apply rotation
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip the weapon's sprite if necessary
        bool shouldFlipWeaponSprite = playerTransform.localScale.x < 0;
        GetComponent<SpriteRenderer>().flipY = shouldFlipWeaponSprite;
        return shouldFlipWeaponSprite;
        
    }
}
