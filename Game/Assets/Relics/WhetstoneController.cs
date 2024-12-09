using NUnit.Framework.Internal.Commands;
using Unity.VisualScripting;
using UnityEngine;

public class WhetstoneController : ItemEffect
{
    public override void Apply(GameObject Target)
    {
        Target.GetComponent<PlayerControl>().critChance += 10;
    }
}
