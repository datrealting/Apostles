using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private GameObject statusEffectHolder;

    private Dictionary<BaseSE, GameObject> statusIcons = new Dictionary<BaseSE, GameObject>();

    public void UpdateHealthBar(float curhp, float maxhp)
    {
        bar.value = curhp / maxhp;
    }
    public void AddStatusIcon(BaseSE effectToAdd)
    {
        Debug.Log("Adding effect: " + effectToAdd);
        GameObject icon = Instantiate(Resources.Load<GameObject>("IconHolder"), statusEffectHolder.transform);
        icon.GetComponent<SpriteRenderer>().sprite = effectToAdd.effectSprite;

        statusIcons[effectToAdd] = icon;
    }
    public void RemoveStatusIcon(BaseSE effectToRemove)
    {
        if (statusIcons.TryGetValue(effectToRemove, out GameObject icon))
        {
            // Destroy the icon GameObject
            Destroy(icon);

            // Remove the reference from the dictionary
            statusIcons.Remove(effectToRemove);
        }
        else
        {
            Debug.LogWarning("Effect not found: " + effectToRemove);
        }
    }
}
