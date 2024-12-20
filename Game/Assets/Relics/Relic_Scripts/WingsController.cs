using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ItemPickups/Wings")]
public class WingsController : ItemEffect
{
    public override void Apply(GameObject Target)
    {
        Target.GetComponent<PlayerControl>().maxhp += 1;
        Target.GetComponent<PlayerMove>().moveSpeed *= 1.5f;
    }
    

}