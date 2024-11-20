using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float range = 0f;
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
        RaycastHit2D[] hits = Physics2D.CircleCastAll(attackTransform.position, range, transform.right, 0f);
        

        for (int i = 0; i < hits.Length; i++)
        {
            NPCStats npcStats = hits[i].collider.GetComponent<NPCStats>();

            if (npcStats != null)
            {
                npcStats.TakeDamage(10);
            }
        }
    }
}
