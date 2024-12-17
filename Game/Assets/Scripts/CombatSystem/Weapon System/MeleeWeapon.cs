using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float range = 0f;
    public float quarterCircleAngle = 90f; // Angle of the quarter circle in degrees
    public int rayCount = 10; // Number of rays to cast within the quarter circle
    public Transform attackTransform;

 

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
            Debug.DrawRay(attackTransform.position, direction * range, Color.red, 0.2f);

            if (hit.collider != null)
            {
                Debug.Log("[Attack] Hit: " + hit.collider.name + " at distance: " + hit.distance);

                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<NPCStats>()?.TakeDamage(weaponStats.dmg);  // Use the damage passed from the weapon
                    GameObject.Find("Player").GetComponent<PlayerControl>().onStrike?.Invoke(hit.collider.gameObject);
                }
            }
            else
            {
                Debug.Log("[Attack] No hit detected for ray at angle: " + angle);
            }
        }
    }
}
