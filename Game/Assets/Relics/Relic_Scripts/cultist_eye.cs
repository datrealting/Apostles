using UnityEngine;

[CreateAssetMenu(menuName = "ItemPickups/Eye")]
public class NewScriptableObjectScript : ItemEffect{
    public GameObject target;
    protected GameObject caller;
    public BaseSE effect;
    public GameObject spritePrefab;

    public override void Apply(GameObject Target)
    {
        GameObject.Find("Player").GetComponent<PlayerControl>().onStrike += EyeEffect;
    }
    
    public void EyeEffect(GameObject target){
        StatusEffectManager.ApplyEffect(target, caller, new CallOfTheEye() , Resources.Load<GameObject>("eye"));
    }
}
