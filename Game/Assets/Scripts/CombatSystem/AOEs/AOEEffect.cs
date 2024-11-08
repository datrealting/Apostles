using System.Collections;
using UnityEngine;

public class AOEEffect : MonoBehaviour
{
    public float duration;  // 0 for an instantaneous one-off effect, time in s longer than 0 for an area denial-type effect
    public float delay = 1;     // the time, in s, until effect takes place

    public LayerMask targetLayer;
    private IAoeEffect[] effects;

    void Start()
    {
        Debug.Log("AOEEffect.cs is being started");
        effects = GetComponents<IAoeEffect>();
        Debug.Log("There are: " + effects.Length + " effects!");
        if (delay > 0)
        {
            // start delay
            StartCoroutine(ApplyDelay(delay));
        }
    }
    
    private IEnumerator ApplyDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // do effect
        Debug.Log("Delay is working, now add effect stupid");
    }
    protected void ApplyEffects(Collider2D[] targets)
    {
        foreach (Collider2D target in targets)
        {
            foreach (IAoeEffect effect in effects)
            {
                effect.ApplyEffect(target);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
