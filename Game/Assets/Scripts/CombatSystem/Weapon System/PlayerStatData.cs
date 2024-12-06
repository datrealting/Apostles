
using UnityEngine;

public class PlayerStatData
{
    // a number that is added to all stat increases for balance
    private static float statIncreaseBias = 1f;

    public int currenthp = 3;

    public int maxhp = 3;
    private int hpincrement = 1;
    public int hpUpgradeCost = 1200;
    public float hpUpgradeCostIncrease = 4.5f + statIncreaseBias;

    public float speed = 3;
    private float speedIncrement = 0.2f;
    public int speedUpgradeCost = 150;
    public float speedUpgradeCostIncrease = 0.9f + statIncreaseBias;

    public float invincibilityTime = 0.5f; // in s

    public float bleedChance;

    public float critChance;

    public void UpgradeMaxHP()
    {
        maxhp += hpincrement;
        currenthp += hpincrement;
        hpUpgradeCost = Mathf.RoundToInt(hpUpgradeCost * hpUpgradeCostIncrease);
    }
    public void UpgradeSpeed()
    {
        speed += speedIncrement;
        speedUpgradeCost = Mathf.RoundToInt(speedUpgradeCost * speedUpgradeCostIncrease);
    }
}
