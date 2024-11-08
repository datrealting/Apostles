using UnityEngine;

public class DamageEffect : MonoBehaviour, IAoeEffect
{
    public float damageAmount;

    public void ApplyEffect(Collider2D target)
    {
        NPCStats npc = target.GetComponent<NPCStats>();
        if (npc != null)
        {
            npc.TakeDamage(damageAmount);
        }
    }
}
