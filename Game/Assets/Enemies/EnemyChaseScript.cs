using System.Security.Cryptography;
using UnityEngine;

public class EnemyChaseScript : MonoBehaviour
{
    public GameObject target;

    public float maxSpeed = 3f;
    public float speed = 3f;

    public Behaviour curBehaviour;

    private void Awake()
    {
        target = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (curBehaviour == Behaviour.Chase)
        {
            Chase();
        }
        CheckState();
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    
    Behaviour CheckState()
    {
        return Behaviour.Chase;
    }
}
public enum Behaviour { 
    Chase, 
    Strafe, 
    Flee 
}