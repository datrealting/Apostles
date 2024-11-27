using UnityEngine;

public class TEMPAOESpawner : MonoBehaviour, IAoe
{
    public GameObject aoePrefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AOEController.SpawnAOE(this, aoePrefab, gameObject.transform.position, 20, 1, 0);
        }
    }
    public void AOEEffect(Collider2D[] hits)
    {

    }
}
