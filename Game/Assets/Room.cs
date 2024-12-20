using UnityEditor.Tilemaps;
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
    [SerializeField]
    private GameObject[] enemies;

    private GameObject obstacle;
    private GameObject enemy;

    private void Awake()
    {
        obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], this.transform.position, Quaternion.identity);
        obstacle.transform.SetParent(this.transform);

        if (enemies.Length > 0)
        {
            enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], this.transform.position, Quaternion.identity);
            enemy.transform.SetParent(this.transform);
            enemy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.SetActive(true);
        }
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

    public void SetEntrance(int dirIndex)
    {
        entrances[dirIndex].SetActive(true);
        walls[dirIndex].SetActive(false);
    }

    public void SetEntranceExit(int exit = -1, int entrance = -1)
    {
        entrances[exit].SetActive(true);
        walls[exit].SetActive(false);

        if (entrance != -1)
        {
            entrances[entrance].SetActive(true);
            walls[entrance].SetActive(false);
        }

        if (exit != -1)
        {
            entrances[exit].SetActive(true);
            walls[exit].SetActive(false);
        }

        //Instantiate(obstacles[Random.Range(0,obstacles.Length)], this.transform.position, Quaternion.identity);
    }
}
