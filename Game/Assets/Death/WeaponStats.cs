
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
