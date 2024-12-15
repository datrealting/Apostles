
using System.Collections;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class Burning : BaseSE
{
    public override OverrideType overrideType => OverrideType.Refresh;

    public override float duration { get; set; } = 5f;  // Settable in this class
    public override float tickFrequency => 1f;

    public float damagePerTick = 6f;

    public override void OnApply()
    {
        //Debug.Log("Additional logic for BURNING application!");
        //sprite = Instantiate(spritePrefab, target.transform);

    }
    public override void OnTick()
    {
        if (targetStats != null)
        {
            targetStats.TakeDamage(damagePerTick);
        }
        if (targetStats == null)
        {
            //Debug.Log("Something died with BURNING on");
        }
    }
    public override void OnExpire()
    {
        //Debug.Log("Additional logic for BURNING removal!");
    }
    public override void OnDie()
    {

    }
}  
