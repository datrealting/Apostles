using System.Runtime;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    public static bool ApplyEffect(GameObject npc, BaseSE effect, GameObject spritePrefab)
    {
        try
        {
            BaseSE newEffect = npc.AddComponent(effect.GetType()) as BaseSE;
            newEffect.Initialise(npc, spritePrefab);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
