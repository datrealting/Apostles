
using System.Collections;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class Exsanguinating : BaseSE
{
    public override OverrideType overrideType => OverrideType.Maxwellian;

    public override float duration { get; set; } = 3f;  // Settable in this class
    public override float tickFrequency => 0.5f;

    public float damagePerTick = 25f;
    protected float playerBleedDamage = 0f;
    public override void OnApply()
    {

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
