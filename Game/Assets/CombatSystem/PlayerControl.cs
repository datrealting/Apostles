using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float dmg = 6;
    public GameObject target;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        RotateTowardsMouse();

        if (Input.GetKeyDown(KeyCode.V))
        {
            try
            {
                target.GetComponent<NPCStats>().TakeDamage(dmg);
                Debug.Log(target.GetComponent<NPCStats>().currenthp + " / " + target.GetComponent<NPCStats>().maxhp);
            }
            catch
            {
                Debug.Log("Target is invalid");
            }
        }
    }

    void RotateTowardsMouse()
    {
        // Get the mouse position in world space and subtract the player position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle from the player to the mouse and rotate the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
