
using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    /* Format:
        int level
        int/float baseAmount
        int/float amount
        int/float amountToIncreaseByEachLevel
    */

    // Damage
    [Header("Damage")]
    private int dmgLevel = 1;
    [SerializeField] private int baseDamage = 3;
    [SerializeField] public int dmg = 3;// Damage multiplier for the weapon as a percentage
    [SerializeField] public int dmgLevelIncrement = 2;



    // # of projectiles
    [Header("Projectile Count")]
    private int projectilesLevel = 1;
    [SerializeField] public int baseProjectilesCount = 1;
    [SerializeField] public int projectilesCount = 1;
    [SerializeField] public int projectilesLevelIncrement = 1;

    // Projectile spread
    [Header("Spread")]
    private int spreadLevel = 1;
    [SerializeField] public float baseSpread = 10f;
    [SerializeField] public float spread = 10f;
    [SerializeField] public float spreadLevelIncrement = 5f;


    // Attack speed
    [Header("Attack Speed")]
    private int atkSpeedLevel = 1;
    [SerializeField] public float baseAtkSpeed = 1f;
    [SerializeField] public float atkSpeed = 0.0001f; // ATKSpeed multiplier for the weapon as a percentage. Higher numbers means higher atk speed
    [SerializeField] public float atkSpeedIncrement = 1f;

    // Projectile speed
    [Header("Projectile Speed")]
    private int projectileSpeedLevel = 1;
    [SerializeField] public float baseProjectileSpeed = 20f;
    [SerializeField] public float projectileSpeed = 20f;
    [SerializeField] public float projectileSpeedIncrement = 5f;

    // Projectile range
    [Header("Projectile Range")]
    private int projectileRangeLevel = 1;
    [SerializeField] public float baseProjectileRange = 10f;
    [SerializeField] public float projectileRange = 10f;
    [SerializeField] public float projectileRangeIncrement = 5f;


    public void LevelUp()
    {
        Debug.Log("Not yet implemented");
        LevelUpSpread();
    }

    public void LevelUpDmg()
    {
        dmgLevel++;
        dmg = dmgLevelIncrement * dmgLevel + baseDamage;
    }

    public void LevelUpProjectiles()
    {
        projectilesLevel++;
        projectilesCount = projectilesLevelIncrement * projectilesLevel + baseProjectilesCount;
    }

    public void LevelUpSpread()
    {
        spreadLevel++;
        spread = spreadLevelIncrement * spreadLevel + baseSpread;
    }

    public void LevelUpAtkSpeed()
    {
        atkSpeedLevel++;
        atkSpeed = atkSpeedIncrement * atkSpeedLevel + baseAtkSpeed;
    }

    public void LevelUpProjectileSpeed()
    {
        projectileSpeedLevel++;
        projectileSpeed = projectileSpeedIncrement * projectileSpeedLevel + baseProjectileSpeed;
    }

    public void LevelUpProjectileRange()
    {
        projectileRangeLevel++;
        projectileRange = projectileRangeIncrement * projectileRangeLevel + baseProjectileRange;
    }

}

