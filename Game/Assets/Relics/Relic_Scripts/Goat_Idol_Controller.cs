using UnityEngine;
[CreateAssetMenu(menuName = "ItemPickups/GoatIdol")]
public class Goat_Idol_Controller : ItemEffect
{
    public override void Apply(GameObject Target){
        Target.GetComponent<PlayerControl>().maxhp /= 2;
        Target.GetComponent<PlayerControl>().currenthp /= 2;
        Target.GetComponent<PlayerControl>().relicAtkspeedMult *= 2;
    }
}
