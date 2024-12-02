using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public PlayerStatData psd;

    // PSD 
    public int maxhp = 3;
    public int currenthp = 3;
    private int minhp = 0;
    public float invincibilityTime = 0.5f; // in s
    public float bleedChance;
    public float critChance;

    // After taking a hit, player should be invincible for a bit
    public GameObject shieldSprite;

    public bool invincible = false;

    // Movement, arbritrary (way too many r's in there btw) number for now
    public float movespeed = 10;

    public AudioSource deathsound;

    private void Start()
    {
        // Overwrite player PSD with PSD saved in GameManager
        LoadFromGameManager();
    }

    private void LoadFromGameManager()
    {
        if (GameManager.Instance != null && GameManager.Instance.psd != null)
        {
            Debug.LogWarning("PSD is found, loading from PSD...");
            psd = GameManager.Instance.psd;
            SetStatsFromPSD();
        }
        else
        {
            Debug.LogWarning("GameManager or PlayerStatsData is missing!");
        }
    }
    private void SaveToGameManager()
    {
        // PROBABLY DONT NEED THIS UNLESS YOU'RE META-PROGRESSING INGAME
    }

    private void SetStatsFromPSD()
    {
        maxhp = psd.maxhp;
        currenthp = psd.currenthp;
        bleedChance = psd.bleedChance;
        critChance = psd.critChance;
        invincibilityTime = psd.invincibilityTime;

        Debug.Log("Player stats successfully loaded from psd!");
    }

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