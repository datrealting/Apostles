using UnityEngine;

public class DELETEMEAOETEST : MonoBehaviour
{
    public GameObject aoePrefab;       // The AoE prefab to spawn
    public float aoeDistance = 10;   // Distance in front of the player
    public float aoeSize = 0.2F;       // Size of the AoE effect
    public string aoeType = "Circle";  // Type of AoE (e.g., Circle, Rectangle, Cone)
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnAoE();
        }
    }

    void SpawnAoE()
    {
        // Calculate spawn position in front of the player
        Vector2 spawnPosition = (Vector2)transform.position + (Vector2)transform.right * aoeDistance;

        // Instantiate the AoE prefab
        GameObject aoeObject = Instantiate(aoePrefab, spawnPosition, Quaternion.identity);

        // Configure the AoE (assuming it has an AoEConfig component)
        AOEConfig config = aoeObject.GetComponent<AOEConfig>();
        if (config != null)
        {
            config.ConfigureAoE(aoeType, aoeSize);
        }
    }
}
