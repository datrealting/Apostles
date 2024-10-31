using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2 roomDimensions;

    private void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Vector3 center = transform.position;

        Gizmos.DrawWireCube(center, new Vector3(roomDimensions.x,roomDimensions.y,0));
    }
}
