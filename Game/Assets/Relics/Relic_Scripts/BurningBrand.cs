using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "ItemPickups/BurningBrand")]
public class BurningBrand : ItemEffect
{
    public GameObject effect;
    public GameObject effectSprite;
    void Awake()
    {
        vName = "Tempo Blade";
        description = "A temporal blade, that constantly flickers in and out of sight in the wielder's hand.";
    }
    public override void Apply(GameObject Target)
    {
        owner = Target;
        Target.GetComponent<PlayerMove>().AddRelicAddSpeed(3f);
        Target.GetComponent<PlayerMove>().AdjustMoveSpeed();
        Target.GetComponent<PlayerControl>().relicAtkspeedMult += 1f;
        GameObject.Find("Player").GetComponent<PlayerControl>().onStrike += IncreaseSpeed;
    }
    
    public void IncreaseSpeed(GameObject target)
    {
        owner.GetComponent<PlayerMove>().AddRelicMultSpeed(0.001f);
        owner.GetComponent<PlayerControl>().relicAtkspeedMult += 0.001f;
    }
}