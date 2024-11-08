using UnityEngine;

public class AOEConfig : MonoBehaviour
{
    public GameObject visual;             // Reference to the visual child object
    public Collider2D colliderComponent;  // Reference to the collider component

    // Method to configure AoE shape and size dynamically. Defaults to circle
    public void ConfigureAoE(string shapeType, float size)
    {
        switch (shapeType)
        {
            case "Circle":
                ConfigureCircle(size);
                break;
        }
    }

    private void ConfigureCircle(float radius)
    {
        // Set collider to circle shape
        var circleCollider = colliderComponent as CircleCollider2D;
        if (circleCollider != null)
        {
            circleCollider.radius = radius;
        }

        // Adjust visual representation
        visual.transform.localScale = new Vector3(radius * 2, radius * 2, 1);

        // Do the effect
        CircleAOE circleAOE = new CircleAOE();

    }
}
