
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour, IAoe
{

    public EnemyChaseScript movement;
    public GameObject aoePrefab;
    
    public float attackCD = 2f;
    public float attackSize = 500f;
    public float attackRange = 2f;
    public float attackDelay = 1f;

    // how much to slow down while attacking (%)
    public float attackSlow = 0.02f;
    float tempSpeedBefore;

    private float nextTime;
    public GameObject target;

    private void Awake()
    {
        target = GameObject.Find("Player");
    }

    void Update()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + attackCD;
            Swipe();
        }
    }
    void Swipe()
    {
        AOEController.SpawnAOE(this, aoePrefab, (Vector2)transform.position + (GetVectorToTarget() * 2f), attackSize, attackDelay, 0);
        //Collider2D[] targetsHit = StartCoroutine(AOEController.SpawnAOE(aoePrefab, (Vector2)transform.position + (GetVectorToTarget()*2f), attackSize, attackDelay, 0));
        tempSpeedBefore = movement.speed;
        movement.speed *= movement.speed * attackSlow;
    }
    void SwipeEffect(Collider2D[] hits)
    {
        try
        {
            foreach (Collider2D obj in hits)
            {
                if (obj == null) continue;
                PlayerStats stats = obj.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    Debug.Log("I hit the player!");
                    stats.TakeDamage(1);
                }
            }
        }
        catch
        {
            Debug.Log("Exception has occured");
        }
        finally
        {
            movement.speed = tempSpeedBefore;
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
