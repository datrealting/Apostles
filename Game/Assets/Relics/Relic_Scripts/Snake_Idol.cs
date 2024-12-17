using UnityEngine;

[CreateAssetMenu(menuName = "ItemPickups/snake_idol")]
public class Snake_Idol : ItemEffect
{
    protected GameObject caller;
    public override void Apply(GameObject Target)
    {
        GameObject.Find("Player").GetComponent<PlayerControl>().onStrike += SnakeEffect;
        
    }
    
    public void SnakeEffect(GameObject target){
        StatusEffectManager.ApplyEffect(target, caller, new Bleeding() , Resources.Load<GameObject>("Bleeding"));
    }
}
