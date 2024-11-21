using UnityEngine;

public class WeaponStats
{
    /* Format:
        int level
        int/float amount
        int/float amountToIncreaseByEachLevel
    */

    // Damage
    public int dmgLevel = 1;
    public int dmg = 3; 
    public int dmgLevelIncrement = 2;

    // Firing speed (percentage %)
    public int firingSpeedLevel = 1;
    public float firingSpeed = 1f;
    public float firingSpeedIncrement = 0.1f;

}
