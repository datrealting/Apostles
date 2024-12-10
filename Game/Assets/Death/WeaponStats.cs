
using UnityEngine;

public class WeaponStats
{
    /* Format:
        int level
        int/float baseAmount
        int/float amount
        int/float amountToIncreaseByEachLevel
    */

    // Damage
    public int dmgLevel = 1;
    public int baseDamage = 3;
    public int dmg = 3; 
    public int dmgLevelIncrement = 2;

    // Firing speed (percentage %)
    public int firingSpeedLevel = 1;
    public float baseFiringSpeed = 1f;
    public float firingSpeed = 1f;
    public float firingSpeedIncrement = 0.1f;

    // # of projectiles
    public int projectilesLevel = 1;
    public int baseProjectilesCount = 1;
    public int projectilesCount = 5;
    public int projectilesLevelIncrement = 1;

    // Projectile spread
    public int spreadLevel = 1;
    public float baseSpread = 10f;
    public float spread = 10f;
    public float spreadLevelIncrement = 0.1f;

    public void LevelUp()
    {
        Debug.Log("Not yet implemented");
        projectilesCount = projectilesLevelIncrement + projectilesLevel;
    }

    public void LevelUpDmg()
    {
        dmgLevel++;
        dmg = dmgLevelIncrement * dmgLevel + baseDamage;
    }
    public void LevelUpFiringSpeed()
    {
        firingSpeedLevel++;
        firingSpeed = firingSpeedIncrement * firingSpeedLevel + baseFiringSpeed;
    }

}

