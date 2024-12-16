
using System.Collections;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class Bloodspark : BaseSE
{
    public override OverrideType overrideType => OverrideType.Refresh;

    public override float duration { get; set; } = 9f;  // Settable in this class
    public override float tickFrequency => 1.5f;

    public float damagePerTick = 5f;
    public float finalDamageHit = 50f;

    private BaseSE bleed = null;
    private GameObject bleedPrefab = null;

    private void Awake()
    {
        bleed = new Bleeding();
        bleedPrefab = Resources.Load<GameObject>("BleedingPrefab");
    }
    public override void OnApply()
    {

    }
    public override void OnTick()
    {
        if (targetStats != null)
        {
            targetStats.TakeDamage(damagePerTick);
            StatusEffectManager.ApplyEffect(target, null, bleed, bleedPrefab);
        }
    }
    public override void OnExpire()
    {
        targetStats.TakeDamage(finalDamageHit);
    }
    public override void OnDie()
    {

    }
}  
