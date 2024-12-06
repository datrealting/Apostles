
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    // boilerplate, feel free to add/remove stats
    public float maxhp;
    public float currenthp;
    private float minhp = 0;

    public float armour;

    public int soulsDropped;
    // The max is slightly higher than minimum just to promote higher soul drops
    public float soulRandomSpreadMin = 0.9f; // as a percentage
    public float soulRandomSpreadMax = 1.2f; // as a percentage

    // use TakeDamage() for most gameplay interactions where armour will be factored in,
    // and AdjustHP() for if you just need to change HP no bullshit 
    public virtual void TakeDamage(float damage)
    {
        float actualdamage = damage - armour;
        if (actualdamage < 1)
        {
            actualdamage = 1;
        }
        currenthp = currenthp - actualdamage;
        if (currenthp <= minhp)
        {
            currenthp = minhp;
            Die();
        }
        if (currenthp > maxhp)
        {
            currenthp = maxhp;
        }
    }
    public virtual void AdjustHP(float hp)
    {
        currenthp = currenthp - hp;
        if (currenthp <= minhp)
        {
            currenthp = minhp;
            Die();
        }
        if (currenthp > maxhp)
        {
            currenthp = maxhp;
        }
    }
    public virtual void Heal(float hp)
    {
        currenthp += hp;
        if (currenthp > maxhp)
        {
            currenthp = maxhp;
        }
    }
    public int RandomSoulDrop()
    {
        return Mathf.RoundToInt(Random.Range(soulsDropped * soulRandomSpreadMin, soulsDropped * soulRandomSpreadMax));
    }
    protected virtual void Die()
    {
        if (GameManager.Instance != null)
        {
            Debug.Log("Dropped: " + GameManager.Instance.AddSouls(RandomSoulDrop()).ToString() + " souls!");
        }
        Destroy(gameObject);
    }

}
