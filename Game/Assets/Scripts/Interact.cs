using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject FloatingTextPrefab; // Reference to the floating text prefab
    [SerializeField] private Vector3 floatingTextOffset = new Vector3(0f, 1f, 0f); // Offset for floating text position
    [SerializeField] private Vector3 floatingTextScale = new Vector3(1f, 1f, 1f); // Scale for floating text

    private GameObject floatingTextInstance;
    public Animator animator;

    // This method will be used to show the floating text when the player enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player that entered
        {
            animator.SetTrigger("TextSpawn");
            ShowFloatingText();
        }
    }

    // This method will remove the floating text when the player exits the trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (floatingTextInstance != null)
            {
                animator.SetTrigger("TextDie");

                // Call DestroyFloatingText after the "TextDie" animation duration
                float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("DestroyFloatingText", animationLength); // Invoke delay before destroying
            }
        }
    }

    // Show floating text above the object
    void ShowFloatingText()
    {
        if (floatingTextInstance != null)
        {
            Destroy(floatingTextInstance); // Destroy any existing text before showing a new one
        }

        // Add the offset to the object's position
        Vector3 spawnPosition = transform.position + floatingTextOffset;

        // Instantiate the floating text prefab
        floatingTextInstance = Instantiate(FloatingTextPrefab, spawnPosition, Quaternion.identity);

        // Set the text (you can customize the text as needed, e.g., "Press E to interact")
        floatingTextInstance.GetComponent<TextMesh>().text = "E";

        // Set the scale of the floating text
        floatingTextInstance.transform.localScale = floatingTextScale;
    }

    // Destroy the floating text
    void DestroyFloatingText()
    {
        Destroy(floatingTextInstance);
    }
}
