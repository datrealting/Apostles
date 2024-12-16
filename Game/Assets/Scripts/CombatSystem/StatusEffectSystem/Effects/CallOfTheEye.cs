
using System.Collections;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class CallOfTheEye : BaseSE
{
    public override OverrideType overrideType => OverrideType.Extend;

    public override float duration { get; set; } = 10f;  // Settable in this class
    public override float tickFrequency => 0.5f;

    // Call of the Eye accumulates 30% of the damage the target takes over the duration. At the end, the target
    // takes that damage again.
    protected float damagePercent = 0.3f;
    protected float accumulatedDamage = 0f;
    public void AddDamage(float damage)
    {
        accumulatedDamage += damage * damagePercent;
        Debug.Log("Accumulated Damage = " + accumulatedDamage);
    }
    public override void OnApply()
    {
        targetStats.onEnemyDamage += AddDamage;
    }
    public override void OnTick()
    {
        
    }
    public override void OnExpire()
    {
        Debug.Log(targetStats);
        targetStats.TakeDamage(accumulatedDamage);
    }
    public override void OnDie()
    {

    }
}
