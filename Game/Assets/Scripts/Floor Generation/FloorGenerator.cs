using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] rooms;
    [SerializeField]
    private int roomCount;
    HashSet<Vector2> visited = new HashSet<Vector2>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GeneratePath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GeneratePath()
    {
        Vector2[] directions =
        {
            new Vector2(0f, 1f),   // Up
            new Vector2(1f, 0f),   // Right
            new Vector2(-1f, 0f),  // Left
        };

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
