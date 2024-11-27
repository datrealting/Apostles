using UnityEngine;
using System.Collections;

/* HOW TO USE:
 * 
 * 1.  Call SpawnAOE(GameObject aoeprefab, Vector2 location, float size, float delay, float linger) 
 * 2. 
 * 
 * AOE Prefab: the prefab to be used (default is Scripts/CombatSystem/AOE System/aoePrefab)
 * Location: the vector2 position that it is centered on
 * Size: the vague size of it
 * Delay: the time before the AOE detonates after the indicator spawns
 * Linger: NOT FUNCTIONING
 */



public class AOE : MonoBehaviour
{
    // Variables
    private Collider2D indicatorCollider;
    private SpriteRenderer indicatorSprite;
    private Transform indicatorTransform;
    public ContactFilter2D contactFilter;  // Configure this in the Inspector if needed

    public float size;      // how vaguely big is it
    // add more parameters for more complex shapes. Circle for now will suffice

    public float delay;     // how long until indicator disappears and AOE takes place?
    public float linger;      //  in seconds, how long AOE lasts. 0 for instantaneous.

    public IAoe caller;
    public Collider2D[] hits;

    public void Create(IAoe caller, float size, float delay, float linger)
    {
        SetupIndicator(size);
        this.delay = delay;
        this.linger = linger;
        this.caller = caller;
        StartCoroutine(DelayedAOE()); 
    }

    void SetupIndicator(float size)
    {
        this.size = size;
        indicatorTransform = transform.Find("IndicatorSprite"); // find the sprite transform
        indicatorSprite = indicatorTransform.GetComponent<SpriteRenderer>();    // find the sprite visuals
        indicatorCollider = gameObject.GetComponent<Collider2D>();

        // Configure size and set sprite to appropriate (Circle/Rectangle/Cone etc.)
        if (indicatorCollider is BoxCollider2D boxCollider)
        {
            boxCollider.size = new Vector2(size, size);
        }
        else if (indicatorCollider is CircleCollider2D circleCollider)
        {
            //Debug.Log("Circle spawned");
            circleCollider.radius = size / 100;  // Set radius for a circle collider
            indicatorTransform.localScale = new Vector3(size/100, size/100, 1);
        }
        else
        {
            Debug.LogWarning("AOE: Unsupported collider type. Complain to Alfie as he's broken something obviously.");
        }
    }
    private IEnumerator DelayedAOE() 
    {
        // Wait for the delay time
        yield return new WaitForSeconds(delay);

        // Trigger the AOE effect after the delay
        TriggerAOEEffect();

        // If the AOE has a linger, disable it after the linger
        if (linger > 0)
        {
            yield return new WaitForSeconds(linger);
            DisableAOE();
        }
    }
    private void TriggerAOEEffect()
    {
        // Code to activate the AOE effect
        // Debug.Log("AOE blows up at " + gameObject.transform.position.x + ", " + gameObject.transform.position.y + " with size: " + size);
        indicatorSprite.enabled = false; // Hide indicator when AOE takes place
        indicatorCollider.enabled = true; // Enable collider if needed for the effect

        // Code to get all hit
        hits = CheckObjectsInArea();
        caller.AOEEffect(hits);
    }
    private Collider2D[] CheckObjectsInArea()
    {
        Collider2D[] results = new Collider2D[20]; // Adjust size as needed
        int numObjects = indicatorCollider.Overlap(contactFilter, results);
        return results;
    }
    private void DisableAOE()
    {
        // Code to disable or destroy the AOE
        Debug.Log("AOE effect ended.");
        indicatorCollider.enabled = false;
        Destroy(gameObject); // Destroy AOE object if you want it to disappear
    }
}
