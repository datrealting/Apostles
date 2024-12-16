using UnityEngine;

public class Orbiting_behaivours : MonoBehaviour
{
    public GameObject target;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.back , 40 * Time.deltaTime);
    }
}
