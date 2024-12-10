
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    // Boilerplate, feel free to add/remove stats
    public float maxhp;
    public float currenthp;
    private float minhp = 0;

    public float armour;
    public GameObject FloatingTextPrefab;

    public int soulsDropped;
    // The max is slightly higher than minimum just to promote higher soul drops
    public float soulRandomSpreadMin = 0.9f; // as a percentage
    public float soulRandomSpreadMax = 1.2f; // as a percentage

    // Use TakeDamage() for most gameplay interactions where armour will be factored in,
    // and AdjustHP() for if you just need to change HP no bullshit 
    public virtual void TakeDamage(float damage)
    {
        float actualdamage = damage - armour;
        if (actualdamage < 1)
        {
            actualdamage = 1;
        }

        // Call the ShowFloatingText method and pass the actualdamage
        if (FloatingTextPrefab)
        {
            ShowFloatingText(actualdamage);
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

    [SerializeField] private Vector3 floatingTextOffset = new Vector3(0f, 1f, 0f); // Offset to control position
    [SerializeField] private Vector3 floatingTextScale = new Vector3(1f, 1f, 1f); // Scale to control size

    void ShowFloatingText(float damageAmount)
    {
        // Add the offset to the enemy's position
        Vector3 spawnPosition = transform.position + floatingTextOffset;

        // Instantiate the floating text without attaching it to the enemy
        var go = Instantiate(FloatingTextPrefab, spawnPosition, Quaternion.identity);

        // Set the text to show the damage amount
        go.GetComponent<TextMesh>().text = damageAmount.ToString();

        // Set the scale of the floating text in world space
        go.transform.localScale = floatingTextScale;
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
        BaseSE[] existingEffects = GetComponents<BaseSE>();
        foreach (BaseSE effect in existingEffects)
        {
            Debug.Log(effect);
            effect.OnDie();
        }
        Destroy(gameObject);
    }
}
