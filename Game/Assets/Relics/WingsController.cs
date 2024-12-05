using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ItemPickups")]
public class WingsController : ItemEffect
{
    public override void Apply(GameObject Target)
    {
        Target.GetComponent<PlayerStats>().maxhp += 1;
        Target.GetComponent<PlayerMove>().moveSpeed *= 1.5f;
    }
    

}