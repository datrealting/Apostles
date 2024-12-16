using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "ItemPickups/TempoBlade")]
public class TBController : ItemEffect
{
    float atkspeed = 0f;
    float speed = 0f;
    float increment = 0.002f;
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
        if (speed < 1)
        {
            owner.GetComponent<PlayerMove>().AddRelicMultSpeed(increment);
            speed += increment;
        }
        if (atkspeed < 1)
        {
            owner.GetComponent<PlayerControl>().relicAtkspeedMult += increment;
            atkspeed += increment;
        }

    }
}