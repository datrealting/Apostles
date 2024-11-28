using UnityEngine;

public class NPCStats : MonoBehaviour
{
    // boilerplate, feel free to add/remove stats
    public float maxhp;
    public float currenthp;
    private float minhp = 0;

    public float armour;

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
    protected virtual void Die()
    {
        // death logic goes here
        Debug.Log("Death has occured!");
        Destroy(gameObject);
    }
}
