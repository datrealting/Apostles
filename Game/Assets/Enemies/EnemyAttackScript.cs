
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
        Debug.Log("Swipe start...");
        AOEController.SpawnAOE(this, aoePrefab, (Vector2)transform.position + (GetVectorToTarget() * 2f), attackSize, attackDelay, 0);
        //Collider2D[] targetsHit = StartCoroutine(AOEController.SpawnAOE(aoePrefab, (Vector2)transform.position + (GetVectorToTarget()*2f), attackSize, attackDelay, 0));

        tempSpeedBefore = movement.speed;
        Debug.Log("Speed OK");
        movement.speed *= movement.speed * attackSlow;

    }
    void SwipeEffect(Collider2D[] hits)
    {
        Debug.Log("SWIIIIPPPPEE!!!!");
        try
        {
            foreach (Collider2D obj in hits)
            {
                if (obj == null) continue;
                PlayerStats stats = obj.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    Debug.Log("I hit the player!");
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
