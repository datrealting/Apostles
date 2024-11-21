
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenuScript : MonoBehaviour
{
    public TMP_Text soulsText;
    public Button damageUpgradeButton;
    public int damageUpgradeCost = 50;

    private void Start()
    {
        UpdateSoulsText();
    }

    void UpdateSoulsText()
    {
        soulsText.text = $"Souls: {GameManager.Instance.playerSouls}";
    }

    public void UpgradeDamage()
    {
        if (GameManager.Instance.playerSouls >= damageUpgradeCost)
        {
            GameManager.Instance.playerSouls -= damageUpgradeCost;
            GameManager.Instance.weaponStats.dmgLevel++;
            UpdateSoulsText();
            Debug.Log($"Weapon damage upgraded to level {GameManager.Instance.weaponStats.dmgLevel}!");
        }
        else
        {
            Debug.Log("Not enough souls!");
        }
    }
}
