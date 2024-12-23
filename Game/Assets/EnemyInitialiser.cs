using Pathfinding;
using UnityEngine;

public class EnemyInitialiser : MonoBehaviour
{
    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found. Make sure a GameObject with the 'Player' tag exists in the scene.");
            return;
        }

        AIDestinationSetter aiSetter = this.GetComponent<AIDestinationSetter>();

        if (aiSetter != null)
        {
            aiSetter.target = player.transform;
        }
        else
        {
            Debug.LogWarning($"AIDestinationSetter component is missing on {gameObject.name}. Enemy won't use AI movement.");
        }

        // Optional: Add any fallback behavior here if AIDestinationSetter is missing
        // For example, you could set the boss to look at the player or move in a custom way
    }
}
