using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ItemPickups/TempoBlade")]
public class TBController : ItemEffect
{
    public override void Apply(GameObject Target)
    {
        Target.GetComponent<PlayerMove>().moveSpeed *= 1.05f;
    }
    

}