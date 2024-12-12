using UnityEngine;
using UnityEngine.UI;

public class ChestOpening : MonoBehaviour

{
    public Text TextUI; // Reference to the UI Text
    public float OffSetPosY = -1f; // Offset to position the text beneath the object

    private bool isOpen;

    public GameObject itemToDrop;
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player"))
        {
            // Convert the world position of the 2D object to world position for the UI
            Vector3 worldPos = transform.position;

            // Set the text position slightly below the object
            /**TextUI.transform.position = new Vector3(worldPos.x, worldPos.y + OffSetPosY, worldPos.z);**/

            Instantiate(itemToDrop, transform.position,transform.rotation);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player"))
        {
            Vector3 worldPos = transform.position;
            /**TextUI.transform.position = new Vector3(worldPos.x, worldPos.y - OffSetPosY, worldPos.z);**/

            
        }
    }

    
}

    

        