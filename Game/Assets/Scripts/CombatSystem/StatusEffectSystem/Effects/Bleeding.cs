
using System.Collections;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class Bleeding : BaseSE
{
    public override OverrideType overrideType => OverrideType.Maxwellian;

    public override float duration { get; set; } = 3f;  // Settable in this class
    public override float tickFrequency => 0.2f;

    public float damagePerTick = 1f;
    protected float playerBleedDamage = 0f;
    public override void OnApply()
    {
        BaseSE[] existingEffects = target.GetComponents<Bleeding>();
        int bleeds = existingEffects.Length;
        if (bleeds >= 10)
        {
            foreach (var bleed in existingEffects)
            {
                bleed.RemoveEffect();
            }
            StatusEffectManager.ApplyEffect(target, null, new Exsanguinating(), Resources.Load<GameObject>("ExsanguinatingPrefab"));
        }
    }
    public override void OnTick()
    {
        if (targetStats != null)
        {
            targetStats.TakeDamage(damagePerTick + playerBleedDamage);
        }
        if (targetStats == null)
        {

        }
    }
    public override void OnExpire()
    {

    }
    public override void OnDie()
    {

    }
}  
