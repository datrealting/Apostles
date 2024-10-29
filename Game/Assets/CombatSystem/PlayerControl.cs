using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float dmg = 6;

    public GameObject target;
    
    void Start()
    {
        
    }

    // Press "V" to 'attack' the target assigned in editor to see how damage works
    // Press mouse click to f
    void Update()
    {
        
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
}
