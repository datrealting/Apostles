using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    // HP / Hearts
    public int maxhp = 5;
    public int currenthp = 5;
    private int minhp = 0;
    public float bleedChance;
    public float critChance;

    // After taking a hit, player should be invincible for a bit
    public GameObject shieldSprite;
    public float invincibilityTime = 1f; // in s
    public bool invincible = false;

    // Movement, arbritrary (way too many r's in there btw) number for now
    public float movespeed = 10;

    public AudioSource deathsound;

    public virtual void TakeDamage(int damage)
    {
        if (!invincible) 
        {
            //Debug.Log("AH DAMAGE");
            currenthp -= damage;
            if (currenthp <= minhp)
            {
                currenthp = 0;
                deathsound.Play();
                StartCoroutine(DelayedDeath(0.8f));
            }
            else
            {
                //Debug.Log("Starting invincibility frames");
                invincible = true; // Set invincible immediately
                shieldSprite.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(iFrames());
            }
        }
    }
    protected virtual void Die()
    {
        // death logic goes here
        // Debug.Log("Death has occured!");
        SceneManager.LoadScene("UpgradeMenu");
    }
    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        Die();
    }
    private IEnumerator iFrames()
    {
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false; // Reset invincibility
        shieldSprite.GetComponent<SpriteRenderer>().enabled = false;
        //Debug.Log("Invincibility ended");
    }
    public virtual void Heal(int healAmount)
    {
        currenthp += healAmount;
        if (currenthp > maxhp)
        {
            currenthp = maxhp;
        }
    }


    public virtual void bleedChanceIncrease(float bleedChanceIncrease)
    {
        bleedChance = bleedChance + bleedChanceIncrease;
    }
}