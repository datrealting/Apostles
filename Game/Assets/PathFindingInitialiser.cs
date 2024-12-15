using UnityEngine;

public class PathFindingInitialiser : MonoBehaviour
{
    void Start()
    {
        AstarPath.active.Scan();
    }
}
