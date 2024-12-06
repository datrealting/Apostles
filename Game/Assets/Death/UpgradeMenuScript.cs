
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeMenuScript : MonoBehaviour
{
    // All the text on screen that changes basically
    public TMP_Text soulsText;
    public TMP_Text hpText;

    public Button hpUpgradeButton;
    public TMP_Text hpCost;

    public Button respawnButton;

    private void Start()
    {
        UpdateSoulsText();
        UpdateHPText();
    }

    void UpdateSoulsText()
    {
        soulsText.text = $"Souls: {GameManager.Instance.playerSouls}";
    }
    void UpdateHPText()
    {
        hpText.text = GameManager.Instance.psd.maxhp.ToString();
        hpCost.text = GameManager.Instance.psd.hpUpgradeCost.ToString();
    }

    public void UpgradeHP()
    {
        Debug.Log("BUTTON IS WORKING");
        if (GameManager.Instance.playerSouls >= GameManager.Instance.psd.hpUpgradeCost)
        {
            GameManager.Instance.playerSouls -= GameManager.Instance.psd.hpUpgradeCost;
            GameManager.Instance.psd.UpgradeMaxHP();
            UpdateHPText();
            UpdateSoulsText();
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
