using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeWeapon : Weapon
{
    public float range = 0f;
    public float weaponSwingArcAngle = 90f; // the  
    // Angle of the quarter circle in degrees
    public int rayCount = 10; // Number of rays to cast within the quarter circle
    public Transform attackTransform;
    private LineRenderer lineRenderer;

    protected override void Start()
    {
        base.Start();
        // Add and configure the LineRenderer component
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = rayCount + 1;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.enabled = false; // Hide the line initially
        lineRenderer.sortingOrder = -1; // Set the order in layer to -1
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        HandleAttackInput();
    }

    public override void Attack()
    {
        float halfQuarterCircleAngle = weaponSwingArcAngle / 2f;
        float angleStep = weaponSwingArcAngle / (rayCount - 1);
        int enemyLayer = LayerMask.GetMask("Enemy");
        HashSet<Collider2D> hitEnemies = new HashSet<Collider2D>();

        // Show the line renderer
        StartCoroutine(ShowLineRenderer());

        for (int i = 0; i < rayCount; i++)
        {
            float angle = -halfQuarterCircleAngle + (angleStep * i);
            Vector2 direction = Quaternion.Euler(0, 0, angle) * attackTransform.up;
            RaycastHit2D hit = Physics2D.Raycast(attackTransform.position, direction, range, enemyLayer);

            // Draw the ray for debugging purposes
            Debug.DrawRay(attackTransform.position, direction * range, Color.red, 0.2f);

            if (hit.collider != null && !hitEnemies.Contains(hit.collider))
            {
                Debug.Log("[Attack] Hit: " + hit.collider.name + " at distance: " + hit.distance);

                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<NPCStats>()?.TakeDamage(weaponStats.dmg);  // Use the damage passed from the weapon
                    GameObject.Find("Player").GetComponent<PlayerControl>().onStrike?.Invoke(hit.collider.gameObject);
                    hitEnemies.Add(hit.collider);
                }
            }
            else
            {
                Debug.Log("[Attack] No hit detected for ray at angle: " + angle);
            }
        }
    }

    private IEnumerator ShowLineRenderer()
    {
        UpdateLineRenderer();
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }

    private void UpdateLineRenderer()
    {
        float halfQuarterCircleAngle = weaponSwingArcAngle / 2f;
        float angleStep = weaponSwingArcAngle / (rayCount - 1);

        lineRenderer.SetPosition(0, attackTransform.position);

        for (int i = 0; i < rayCount; i++)
        {
            float angle = -halfQuarterCircleAngle + (angleStep * i);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * attackTransform.up * range;
            lineRenderer.SetPosition(i + 1, attackTransform.position + direction);
        }
    }
}
