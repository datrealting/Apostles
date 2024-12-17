using System.Security.Permissions;
using UnityEngine;

public class DEMOPLSDELETEgimmethestatus : MonoBehaviour
{
    public GameObject target;
    protected GameObject caller;

    public BaseSE effect;
    public BaseSE effect2;
    public BaseSE effect3;
    public BaseSE effect4;

    public GameObject spritePrefab;
    public GameObject spritePrefab2;
    public GameObject spritePrefab3;
    public GameObject spritePrefab4;

    void Awake()
    {
        caller = gameObject; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject.Find("Player").GetComponent<PlayerControl>().bleedChance += 0.5f;
            /*
            if (effect != null)
            {
                StatusEffectManager.ApplyEffect(target, caller, effect, spritePrefab);
            }
            if (effect2 != null)
            {
                StatusEffectManager.ApplyEffect(target, caller, effect2, spritePrefab2);
            }
            if (effect3 != null)
            {
                StatusEffectManager.ApplyEffect(target, caller, effect3, spritePrefab3);
            }
            if (effect4 != null)
            {
                StatusEffectManager.ApplyEffect(target, caller, effect4, spritePrefab4);
            }
            */
        }
    }
}
