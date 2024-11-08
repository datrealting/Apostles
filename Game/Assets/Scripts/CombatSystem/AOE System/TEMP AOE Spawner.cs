using UnityEngine;

public class TEMPAOESpawner : MonoBehaviour
{
    public GameObject aoePrefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnAOE();
        }
    }

    void SpawnAOE()
    {
        GameObject aoeIndicator = Instantiate(aoePrefab, gameObject.transform.position, Quaternion.identity);
    }
}
