
using UnityEngine;

public class PlayerStatData
{
    // a number that is added to all stat increases for balance
    private static float statIncreaseBias = 1f;

    public int currenthp;

    public float damage;
    private float damageIncrement = 2f;
    public int damageUpgradeCost = 100;
    public float damageUpgradeCostIncrease = 0.5f + statIncreaseBias;

    public float atkspeed;
    private float atkspeedIncrement = 1f;
    public int atkspeedUpgradeCost = 100;
    public float atkspeedUpgradeCostIncrease = 0.5f + statIncreaseBias;

    public int maxhp;
    private int hpincrement = 1;
    public int hpUpgradeCost = 1200;
    public float hpUpgradeCostIncrease = 4.5f + statIncreaseBias;

    public float speed;
    private float speedIncrement = 0.2f;
    public int speedUpgradeCost = 150;
    public float speedUpgradeCostIncrease = 0.9f + statIncreaseBias;

    public float invincibilityTime = 0.5f; // in s

    public float bleedChance;

    public float critChance;
    
    public PlayerStatData()
    {
        Debug.Log("PSD formed from inspector player settings: " + GameObject.Find("Player"));
        PlayerControl pc = GameObject.Find("Player").GetComponent<PlayerControl>();
        PlayerMove pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        damage = pc.dmg;
        atkspeed = pc.atkspeed;
        maxhp = pc.maxhp;
        currenthp = pc.maxhp;
        speed = pm.moveSpeed;
    }
    public void UpgradeDamage()
    {
        damage += damageIncrement;
        damageUpgradeCost = Mathf.RoundToInt(damageUpgradeCost * damageUpgradeCostIncrease);
    }
    public void UpgradeAtkspeed()
    {
        atkspeed += atkspeedIncrement;
        atkspeedUpgradeCost = Mathf.RoundToInt(atkspeedUpgradeCost * atkspeedUpgradeCostIncrease);
    }
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
