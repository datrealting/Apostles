using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ItemPickups")]
public class CrownController : ItemEffect
{
    public override void Apply(GameObject Target)
    {
        Target.GetComponent<PlayerControl>().bleedChance +=0.1f;
    }
    

}