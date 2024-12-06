using UnityEngine;
using UnityEngine.SceneManagement;

public class DEMOPLEASEDELETEdeathmaker : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("NewUpgradeMenu");
        }
    }
}
