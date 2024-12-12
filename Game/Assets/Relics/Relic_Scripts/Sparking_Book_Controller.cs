using System;
using UnityEngine;

public class Sparking_Book_Controller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        Destroy(gameObject);
    }
}

