using System.Security.Permissions;
using UnityEngine;

public class DEMOPLSDELETEgimmethestatus : MonoBehaviour
{
    public GameObject target;
    protected GameObject caller;
    public BaseSE effect;
    public GameObject spritePrefab;

    void Awake()
    {
        caller = gameObject; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StatusEffectManager.ApplyEffect(target, caller, effect, spritePrefab);
        }
    }
}
