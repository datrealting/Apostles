using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string Name;
    [SerializeField] public int Damage;
    [SerializeField][Min(0.01f)] public float AttackRate = 0.5f; // Time in seconds between attacks

    public float Cooldown = 0f;
    public int Level;

    private WeaponLevelHandler weaponLevelHandler = new WeaponLevelHandler();

    public abstract void Attack();

    public void LevelUp()
    {
        // Increase the weapon level
        weaponLevelHandler.LevelUp();
    }


    public void HandleAttackInput()
    {
        // Check if the player is pressing the fire button and if the time has passed since the last shot
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= Cooldown)
        {
            Attack();
            // Set the next fire time
            Cooldown = Time.time + AttackRate;
        }
    }


    private void HandleLevelUpInput()
    {
        //
#if UNITY_EDITOR
        // Check if the player is pressing the L key
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
#endif
    }
}