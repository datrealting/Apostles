using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // HP / Hearts
    public int maxhp = 5;
    public int currenthp = 5;
    private int minhp = 0;

    // After taking a hit, player should be invincible for a bit
    public float invincibilityTime = 1000; // in ms

    // Movement, arbritrary (way too many r's in there btw) number for now
    public float movespeed = 10;

    public virtual void TakeDamage(int damage)
    {
        currenthp -= damage;
        if (currenthp <= minhp)
        {
            currenthp = 0;
            Die();
        }
    }
    protected virtual void Die()
    {
        // death logic goes here
        Debug.Log("Death has occured!");
    }
}
