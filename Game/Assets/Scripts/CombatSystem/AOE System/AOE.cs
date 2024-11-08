using UnityEngine;

public class AOE : MonoBehaviour
{
    // Variables
    private Collider2D indicatorCollider;
    private Sprite indicatorSprite;

    public float size;      // how vaguely big is it
    // add more parameters for more complex shapes. Circle for now will suffice

    public float delay;     // how long until indicator disappears and AOE takes place?
    public float duration;      //  in seconds, how long AOE lasts. 0 for instantaneous.

    void Start()
    {
        size = 20f;
        indicatorSprite = gameObject.GetComponentInChildren<Sprite>();
        indicatorCollider = gameObject.GetComponent<Collider2D>();
        // Configure size
        if (indicatorCollider is BoxCollider2D boxCollider)
        {
            boxCollider.size = new Vector2(size, size);
        }
        else if (indicatorCollider is CircleCollider2D circleCollider)
        {
            circleCollider.radius = size / 2;  // Set radius for a circle collider
        }
        else
        {
            Debug.LogWarning("AOE: Unsupported collider type. Complain to Alfie as he's broken something obviously.");
        }
        Debug.Log(indicatorSprite);

        // Do effect
        Debug.Log("AOE spawned at " + gameObject.transform.position.x + ", " + gameObject.transform.position.y + " with size: " + size);
    }
}
