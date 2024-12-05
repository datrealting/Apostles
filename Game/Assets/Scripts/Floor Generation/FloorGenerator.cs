using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] rooms;
    [SerializeField]
    private int roomCount = 15;
    [SerializeField]
    private int maxDepth = 10;
    private int currentDepth = 0;

    HashSet<Vector2> visited = new HashSet<Vector2>();

    Vector2[] directions =
    {
        new Vector2(0f, 1f),   // Up
        new Vector2(1f, 0f),   // Right
        new Vector2(0f, -1f),   // down
        new Vector2(-1f, 0f),  // Left
    };

    int[] allowedDirectionIndexes = { 0, 1, 2 };


    void Start()
    {
        GenRoom(Vector2.zero, -1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenRoom(Vector2.zero, -1);
        }
    }

    private bool GenRoom(Vector2 pos, int entranceDir)
    {
        if (roomCount <= 0) return false;
        if (visited.Contains(pos)) return false;
        if (currentDepth > maxDepth) return false;

        roomCount--;
        visited.Add(pos);

        GameObject roomObject = rooms[Random.Range(0, rooms.Length)];
        GameObject roomInstance = Instantiate(roomObject, pos, Quaternion.identity);
        roomInstance.transform.SetParent(transform);
        
        Room roomComponent = roomInstance.GetComponent<Room>();

        if(entranceDir != -1)
        {
            roomComponent.SetEntrance(entranceDir);
        }

        int roomConnections = Random.Range(1, 4);
        HashSet<int> visitedDirections = new HashSet<int>();

        currentDepth++;

        for (int i = 0; i < roomConnections; i++)
        {

            int direction = allowedDirectionIndexes[Random.Range(0, allowedDirectionIndexes.Length)];

            while (visitedDirections.Contains(direction))
            {
                direction = allowedDirectionIndexes[Random.Range(0, allowedDirectionIndexes.Length)];
            }

            Debug.Log("EXIT: " + direction);
            Vector2 newRoomPos = pos + (roomComponent.roomDimensions * directions[direction]);

            bool roomCreated = GenRoom(newRoomPos, (direction + 2) % 4);

            if(roomCreated) roomComponent.SetEntrance(direction);

            visitedDirections.Add(direction);
        }

        currentDepth--;

        return true;
    }

    private void GeneratePath()
    {

        Vector2 pos = new Vector2(0f,0f);
        Vector2 direction = directions[0];
        GameObject roomObject = rooms[Random.Range(0, rooms.Length)];
        Room room = roomObject.GetComponent<Room>();

        int exit = 0;
        int entrance = -1;

        for (int i = 0; i < roomCount; i++)
        {
            int dirIndex = Random.Range(0, directions.Length);
            Vector2 newDirection = directions[dirIndex];

            while(newDirection * new Vector2(-1f,-1f) == direction)
            {
                dirIndex = Random.Range(0, directions.Length);
                newDirection = directions[dirIndex];
            }
            direction = newDirection;

            GameObject createdRoom = Instantiate(roomObject, pos, Quaternion.identity);

            if(dirIndex == 2)
            {
                exit = dirIndex+1;
            }
            else
            {
                exit = dirIndex;
            }

            createdRoom.GetComponent<Room>().SetEntranceExit(exit,entrance);
            pos += (room.roomDimensions * direction/2);
            visited.Add(pos);

            entrance = (exit + 2) % 4;


            roomObject = rooms[Random.Range(0, rooms.Length)];
            room = roomObject.GetComponent<Room>();

            pos += (room.roomDimensions * direction / 2);
        }

    }
}
