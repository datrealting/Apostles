using Unity.Mathematics;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0 ,0 , rot - 180);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
