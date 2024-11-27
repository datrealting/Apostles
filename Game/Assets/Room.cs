using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Room : MonoBehaviour
{
    public Vector2 roomDimensions;

    [SerializeField]
    private GameObject[] walls = new GameObject[4];
    [SerializeField]
    private GameObject[] entrances = new GameObject[4];
    [SerializeField]
    private GameObject[] obstacles;

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

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Vector3 center = transform.position;

        Gizmos.DrawWireCube(center, new Vector3(roomDimensions.x,roomDimensions.y,0));
    }*/

    public void SetEntranceExit(int exit,int entrance = -1)
    {
        entrances[exit].SetActive(true);
        walls[exit].SetActive(false);

        if (entrance != -1) {
            entrances[entrance].SetActive(true);
            walls[entrance].SetActive(false);
        }

        Instantiate(obstacles[Random.Range(0,obstacles.Length)], this.transform.position, Quaternion.identity);
    }
}
