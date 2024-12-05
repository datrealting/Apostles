using Unity.Mathematics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject camera;
    private Vector2 roomDimensions = new Vector2(32, 18);

    private void Awake()
    {
        Debug.Log((roomDimensions.x) * (math.floor(-2 / 32)));
        Debug.Log(Mathf.Floor(-2 / 32));
        camera = Camera.main.gameObject;

        UpdateCamera();
    }

    void Update()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector2 playerPos = this.transform.position;

        int roomX = Mathf.FloorToInt((playerPos.x + roomDimensions.x / 2) / roomDimensions.x);
        int roomY = Mathf.FloorToInt((playerPos.y + roomDimensions.y / 2) / roomDimensions.y);

        float cameraX = roomX * roomDimensions.x;
        float cameraY = roomY * roomDimensions.y;

        if (camera.transform.position.x != cameraX || camera.transform.position.y != cameraY)
        {
            camera.transform.position = new Vector3(cameraX, cameraY, -10f);
            GameObject room = null;
            if (FloorGenerator.roomMap.TryGetValue(new Vector2(cameraX, cameraY), out room))
            {
                room.SetActive(true);
            }
        }
    }

}
