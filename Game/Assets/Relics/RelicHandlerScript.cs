using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Relic
{
    public GameObject prefab;   // The prefab to spawn
    public int dropChance;      // Weight for this relic
}
public class RelicHandlerScript : MonoBehaviour
{
    // Public dictionary with relic prefab : drop chance weighting basically
    [SerializeField] private List<Relic> relics = new List<Relic>();
    public GameObject GetRandomDrop()
    {
        // Calculate the total weight (sum of all drop chances)
        float totalWeight = 0f;
        foreach (var relic in relics)
        {
            totalWeight += relic.dropChance;
        }

        // Get a random value between 0 and the total weight
        float randomValue = Random.Range(0, totalWeight);

        // Determine which item corresponds to the random value
        float cumulativeWeight = 0f;
        foreach (var relic in relics)
        {
            cumulativeWeight += relic.dropChance;
            if (randomValue <= cumulativeWeight)
            {
                return relic.prefab; // Return the selected item's prefab
            }
        }

        return null; // Fallback in case no item is selected
    }
}
