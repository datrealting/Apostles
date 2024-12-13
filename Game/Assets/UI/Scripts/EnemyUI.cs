using UnityEngine;
using UnityEngine.UI;
public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider bar;
    public void UpdateHealthBar(float curhp, float maxhp)
    {
        bar.value = curhp / maxhp;
    }
}
