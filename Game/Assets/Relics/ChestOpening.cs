using UnityEngine;
using UnityEngine.UI;

public class ChestOpening : MonoBehaviour

{
    public bool isOpen;
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player"))
        {
            if(isOpen == false){
                gameObject.GetComponent<Chest_interaction>().enabled = true;
                
        }


        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Chest_interaction>().enabled = false;
        }
    }

    
}

    

        