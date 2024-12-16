
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

    public int projcount;
    private int projcountIncrement = 1;
    public int projcountCost = 3000;
    public float projectcountCostIncrease = 1.2f + statIncreaseBias;

    public float projspeed;
    private float projspeedIncrement = 0.5f;
    public int projspeedCost = 300;
    public float projectspeedCostIncrease = 1.2f + statIncreaseBias;

    public float projrange;
    private float projrangeIncrement = 1f;
    public int projrangeCost = 300;
    public float projectrangeCostIncrease = 1.2f + statIncreaseBias;

    public float invincibilityTime = 0.5f; // in s

    public float bleedChance;

    public float critChance;
    
    public PlayerStatData()
    {
        Debug.Log("PSD formed from inspector player settings: " + GameObject.Find("Player"));
        PlayerControl pc = GameObject.Find("Player").GetComponent<PlayerControl>();
        PlayerMove pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        Weapon wep = GameObject.Find("Player").transform.Find("weaponHolder").GetChild(0).GetComponent<Weapon>();

        damage = pc.dmg;
        atkspeed = pc.atkspeed;
        maxhp = pc.maxhp;
        currenthp = pc.maxhp;
        speed = pm.moveSpeed;
        bleedChance = pc.bleedChance;
        projcount = wep.weaponStats.projectilesCount;
        projspeed = wep.weaponStats.projectileSpeed;
        projrange = wep.weaponStats.projectileRange;
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
    public void UpgradeProjcount()
    {
        projcount += projcountIncrement;
        projcountCost = Mathf.RoundToInt(projcountCost * projectcountCostIncrease);
    }
    public void UpgradeProjspeed()
    {
        projspeed += projspeedIncrement;
        projspeedCost = Mathf.RoundToInt(projspeedCost * projectspeedCostIncrease);
    }
    public void UpgradeProjrange()
    {
        projrange += projrangeIncrement;
        projrangeCost = Mathf.RoundToInt(projrangeCost * projectrangeCostIncrease);
    }
}
