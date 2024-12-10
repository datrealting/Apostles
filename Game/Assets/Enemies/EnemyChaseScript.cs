
using System.Security.Cryptography;
using UnityEngine;

public class EnemyChaseScript : MonoBehaviour
{
    public GameObject target;

    public float maxSpeed = 3f;
    public float speed = 3f;
    public float multipliers = 1f; // add percentages / subtract to increase decrease speed 

    public Behaviour curBehaviour;

    private void Awake()
    {
        target = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Move();
    }
    
    void CalculateSpeed()
    { 
        speed = maxSpeed * multipliers;
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
        if (target == null)
        {
            Debug.Log("FUCKY");
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
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