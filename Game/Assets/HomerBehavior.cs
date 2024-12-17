using UnityEngine;
using System.Collections;

public class HomerBehavior : MonoBehaviour
{
    private GameObject target;
    public Transform shootPos;
    public GameObject projectile;
    public GameObject telegraphPos; // Visual indicator for telegraph

    public float distance;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        getDistanceToTarget();
        RangedAttack();
    }

    void RangedAttack()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance < 8f)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                StartCoroutine(SpawnTelegraphAndProjectile());
            }
        }
    }

    void getDistanceToTarget()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        //Debug.Log(distance);
    }

    IEnumerator SpawnTelegraphAndProjectile()
    {
        // Spawn the telegraph at the shoot position
        GameObject spawnTelegraph = Instantiate(telegraphPos, shootPos.position, Quaternion.identity);

        // Wait for 1 second before spawning the projectile
        yield return new WaitForSeconds(.1f);

        // Spawn the projectile at the shoot position
        GameObject newProjectile = Instantiate(projectile, shootPos.position, Quaternion.identity);

        // Optionally, destroy the telegraph after the projectile is spawned
        Destroy(spawnTelegraph);
    }
}