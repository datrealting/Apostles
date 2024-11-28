using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WingsController : MonoBehaviour
{
    public Playermovement playermovement;
    public PlayerHealth health;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            health.maxHealth++;
            playermovement._speed = (float)(playermovement._speed*1.5);
            Destroy(gameObject);
        }
    }

}