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

    public void LevelUp()
    {
        // Increase the weapon level
        weaponStats.LevelUp();
    }


    public void HandleAttackInput()
    {
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
}
