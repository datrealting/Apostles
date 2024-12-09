using System.Security.Permissions;
using UnityEngine;

public class DEMOPLSDELETEgimmethestatus : MonoBehaviour
{
    public GameObject target;
    public BaseSE effect;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StatusEffectManager.ApplyEffect(target, effect);
        }
    }
}
