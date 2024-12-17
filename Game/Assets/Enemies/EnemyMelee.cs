using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class EnemyMelee : MonoBehaviour, IAoe
{
    public EnemyChaseScript movement;
    public GameObject aoePrefab;

    public float attackCD = 3f;
    public float attackSize = 3f;
    public float attackRange = 1.5f;
    public float attackDelay = 1;

    // how much to slow down while attacking (%)
    //public float attackSlow = 0.02f;
    float tempSpeedBefore;
    private float distance;
    private float nextTime;
    public GameObject target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        getDistanceToTarget();
        attack();
    }

    void attack()
    {
        if (Time.time < nextTime)
            return; // Exit if attack is on cooldown

        //Debug.Log($"Attack called. Distance: {distance}");

        if (distance < 3f)
        {
            //Debug.Log("Within attack range");
            Swipe();
            nextTime = Time.time + attackCD; // Set the cooldown
        }
        else
        {
            //Debug.Log("Out of range");
        }
    }

    void getDistanceToTarget()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        //Debug.Log(distance);
    }


    void Swipe()
    {
        AOEController.SpawnAOE(this, aoePrefab, target.transform.position, attackSize, attackDelay, 0);
        //Collider2D[] targetsHit = StartCoroutine(AOEController.SpawnAOE(aoePrefab, (Vector2)transform.position + (GetVectorToTarget()*2f), attackSize, attackDelay, 0));
    }
    void SwipeEffect(Collider2D[] hits)
    {
        foreach (Collider2D obj in hits)
        {
            if (obj == null) continue;
            PlayerControl stats = obj.GetComponent<PlayerControl>();
            if (stats != null)
            {
                //Debug.Log("I hit the player!");
                stats.TakeDamage(1);
            }
        }
    }
    public void AOEEffect(Collider2D[] hits)
    {
        SwipeEffect(hits);
    }
    private Vector2 GetVectorToTarget()
    {
        var heading = target.transform.position - transform.position;
        var direction = heading.normalized;
        return direction;
    }
}
