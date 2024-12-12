using UnityEngine;
[CreateAssetMenu(menuName = "ItemPickups/Brew")]
public class Brew_Controller : ItemEffect
{   
    public GameObject target;
    protected GameObject caller;
    public BaseSE effect;
    public GameObject spritePrefab;
    public override void Apply(GameObject Target)
    {
        GameObject.Find("Player").GetComponent<PlayerControl>().onStrike += BrewEffect;
    }
    
    public void BrewEffect(GameObject target){
        StatusEffectManager.ApplyEffect(target, caller, new Hellfire() , Resources.Load<GameObject>("HellFirePrefab"));
    }
}
