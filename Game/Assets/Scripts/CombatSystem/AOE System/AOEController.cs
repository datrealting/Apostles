using System.Collections;
using UnityEditor;
using UnityEngine;

public class AOEController : MonoBehaviour
{

    // TL;DR
    // First argument: prefab to use
    // Second argument: origin of AOE

    static public void SpawnAOE(IAoe caller, GameObject prefab, Vector2 spawnLocation, float size, float delay, float linger)
    {
        GameObject obj = Instantiate(prefab, spawnLocation, Quaternion.identity);
        AOE aoe = obj.GetComponent<AOE>();
        aoe.Create(obj, caller, size, delay, linger);
    }
}
