
using UnityEngine;

public class PlayerStatData
{
    public int maxhp = 3;
    public int currenthp = 3;
    private int minhp = 0;
    public int speed;
    public float invincibilityTime = 0.5f; // in s
    public float bleedChance;
    public float critChance;

    public void UpgradeMaxHP()
    {
        maxhp += 1;
        currenthp += 1;
        Debug.Log("Upgraded Max HP to " + maxhp.ToString() + "!");
    }

}
