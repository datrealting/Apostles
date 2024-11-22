
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeMenuScript : MonoBehaviour
{
    // All the text on screen that changes basically
    public TMP_Text soulsText;
    public TMP_Text wepDmgText;

    public Button damageUpgradeButton;
    public int damageUpgradeCost = 50;

    public Button respawnButton;

    private void Start()
    {
        UpdateSoulsText();
        UpdateDmgText();
    }

    void UpdateSoulsText()
    {
        soulsText.text = $"Souls: {GameManager.Instance.playerSouls}";
    }
    void UpdateDmgText()
    {
        wepDmgText.text = GameManager.Instance.weaponStats.dmg.ToString();
    }

    public void UpgradeDamage()
    {
        if (GameManager.Instance.playerSouls >= damageUpgradeCost)
        {
            GameManager.Instance.playerSouls -= damageUpgradeCost;
            GameManager.Instance.weaponStats.LevelUpDmg();
            UpdateDmgText();
            UpdateSoulsText();
            Debug.Log($"Weapon damage upgraded to level {GameManager.Instance.weaponStats.dmgLevel}!");
        }
        else
        {
            Debug.Log("Not enough souls!");
        }
    }

    // SCENE TRANSITIONS
    public void Respawn()
    {
        SceneManager.LoadScene("Test");
    }
    public void Quit()
    {
        Debug.Log("Pls don't lol");
    }
}
