using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{

    public AIPath aiPath;

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {

            Debug.Log("facing left");
            transform.localScale = new Vector3(-7.5f, 7.5f, 7.5f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            Debug.Log("facing right");
            transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
        }
    }
}
