using Pathfinding;
using UnityEngine;

public class EnemyInitialiser : MonoBehaviour
{
    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        this.GetComponent<AIDestinationSetter>().target = player.transform;
    }
}
