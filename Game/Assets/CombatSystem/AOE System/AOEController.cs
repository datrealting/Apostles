using UnityEditor;
using UnityEngine;

public class AOEController : MonoBehaviour
{

    // TL;DR
    // First argument: prefab to use
    // Second argument: origin of AOE

    static public void SpawnAOE(GameObject prefab, Vector2 spawnLocation, float size)
    {
        GameObject obj = Instantiate(prefab, spawnLocation, Quaternion.identity);
        AOE aoe = obj.GetComponent<AOE>();
        aoe.Create(size);
    }
}
