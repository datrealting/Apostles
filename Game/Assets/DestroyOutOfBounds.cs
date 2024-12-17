using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object leaving the bounds is a projectile
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
