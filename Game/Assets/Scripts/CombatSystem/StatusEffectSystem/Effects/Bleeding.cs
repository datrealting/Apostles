
using System.Collections;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class Bleeding : BaseSE
{
    public override OverrideType overrideType => OverrideType.Maxwellian;

    public override float duration { get; set; } = 3f;  // Settable in this class
    public override float tickFrequency => 0.5f;

    public float damagePerTick = 1f;
    protected float playerBleedDamage = 0f;
    public override void OnApply()
    {
        Debug.Log("Additional logic for BURNING application!");
        sprite = Instantiate(spritePrefab, target.transform);
    }
    public override void OnTick()
    {
        if (targetStats != null)
        {
            targetStats.TakeDamage(damagePerTick + playerBleedDamage);
        }
        if (targetStats == null)
        {
            Debug.Log("Something died with BURNING on");
        }
    }
    public override void OnExpire()
    {
        Debug.Log("Additional logic for BURNING removal!");
    }
    public override void OnDie()
    {

    }
}  
