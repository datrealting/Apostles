using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ItemPickups/Brick")]
public class BrickController : ItemEffect
{
    public override void Apply(GameObject Target)
    {
        Target.GetComponent<PlayerControl>().relicAtkspeedMult += 0.05f;
    }
}
