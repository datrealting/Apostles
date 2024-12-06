
using UnityEngine;

public class PlayerStatData
{
    public int currenthp = 3;
    public int maxhp = 3;

    private int hpincrement = 1;
    public int hpUpgradeCost = 500;
    public float hpUpgradeCostIncrease = 1.4f;

    public int speed;

    public float invincibilityTime = 0.5f; // in s

    public float bleedChance;

    public float critChance;

    public void UpgradeMaxHP()
    {
        maxhp += hpincrement;
        currenthp += hpincrement;
        hpUpgradeCost = Mathf.RoundToInt(hpUpgradeCost * hpUpgradeCostIncrease);
    }

}
