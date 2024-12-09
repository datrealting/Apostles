using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float range = 0f;
    public float quarterCircleAngle = 90f; // Angle of the quarter circle in degrees
    public int rayCount = 10; // Number of rays to cast within the quarter circle
    public Transform attackTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleAttackInput();
    }

    public override void Attack()
    {
        float halfQuarterCircleAngle = quarterCircleAngle / 2f;
        float angleStep = quarterCircleAngle / (rayCount - 1);

        for (int i = 0; i < rayCount; i++)
        {
            float angle = -halfQuarterCircleAngle + (angleStep * i);
            Vector2 direction = Quaternion.Euler(0, 0, angle) * attackTransform.up;
            RaycastHit2D hit = Physics2D.Raycast(attackTransform.position, direction, range);

            // Draw the ray for debugging purposes
            Debug.DrawRay(attackTransform.position, direction * range, Color.red, 1.0f);

            if (hit.collider != null)
            {
                NPCStats npcStats = hit.collider.GetComponent<NPCStats>();

                if (npcStats != null)
                {
                    npcStats.TakeDamage(10);
                }
            }
        }
    }
}
