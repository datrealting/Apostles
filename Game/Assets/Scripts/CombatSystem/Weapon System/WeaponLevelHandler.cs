using UnityEngine;

public class WeaponLevelHandler
{
    [SerializeField]
    [Range(1, 5)]
    private int currentLevel = 1;

    [SerializeField]
    [Min(1)]
    private int maxLevel = 5;

    private const float FireRateDecreaseFactor = 0.9f;

    private const float DamageIncreaseFactor = 1.5f;
    //public int CurrentLevel => currentLevel;

    public bool LevelUp()
    {
        if (currentLevel >= maxLevel)
        {
            return false;
        }

        currentLevel++;
        return true;
    }

    public void DecreaseFireRate(ref float fireRate)
    {
        fireRate *= FireRateDecreaseFactor;
    }

    public void CalculateDamage(ref float Damage)
    {
        Damage *= DamageIncreaseFactor;
    }
}
