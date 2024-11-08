using UnityEngine;

public class CircleAOE : AOEEffect
{
    public float radius;

    void Start()
    {   
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        ApplyEffects(targets);
        Debug.Log("CircleAOE called!");
    }
}
