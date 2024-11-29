using System;
using Unity.VisualScripting;
using UnityEngine;

public class Crown_Of_Thorns_Behaivour : MonoBehaviour
{
    private float bleedChanceIncrease = 0.1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControl player = collision.GetComponent<PlayerControl>();
        if (player != null )
        {
            player.bleedChanceIncrease(bleedChanceIncrease);
            Destroy(gameObject);
        }
        else{
            Debug.Log("oopsie");
        }
        
    }
}
