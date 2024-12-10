using NUnit.Framework.Internal.Commands;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
[CreateAssetMenu(menuName = "ItemPickups/Whetstone")]
public class WhetstoneController : ItemEffect
{
    public override void Apply(GameObject Target)
    {
        Target.GetComponent<PlayerControl>().critChance += 0.1f;
    }
}
