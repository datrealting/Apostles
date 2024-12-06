using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3f;
    public Vector3 offSet = new Vector3(0, 3, 0);
    public Vector3 RandomizePos = new Vector3(-0.5f, 0, 0);

    void Start()
    {
        Destroy(gameObject, destroyTime);

        transform.localPosition += offSet;
        transform.localPosition += new Vector3(Random.Range(-RandomizePos.x, RandomizePos.y),
        Random.Range(-RandomizePos.y, RandomizePos.y),
        Random.Range(-RandomizePos.x, RandomizePos.x));    }
}
