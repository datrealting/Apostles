using NUnit.Framework.Internal;
using UnityEngine;

public class lifetime : MonoBehaviour
{
    public GameObject self;

    // Update is called once per frame
    void Update()
    {
        Destroy(self, 1f);
    }
}
