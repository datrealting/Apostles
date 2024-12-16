using JetBrains.Annotations;
using NUnit.Framework.Internal.Commands;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "ItemPickups/GhostSword")]
public class Ghostly_sword : ItemEffect
{
    private Vector3 offSet = new Vector3(2, 0, 0);
    public override void Apply(GameObject Target){
    GameObject.Instantiate(Resources.Load("sword_orbit"), Target.transform.position + offSet, Target.transform.rotation);
    
}
}