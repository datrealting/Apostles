using UnityEngine;

public class TEMPAOESpawner : MonoBehaviour
{
    public GameObject aoePrefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AOEController.SpawnAOE(aoePrefab, gameObject.transform.position, 20);
        }
    }
}
