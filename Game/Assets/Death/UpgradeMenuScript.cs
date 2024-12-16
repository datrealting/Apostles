
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeMenuScript : MonoBehaviour
{
    // All the text on screen that changes basically
    public TMP_Text soulsText;

    // Text
    public TMP_Text damageText;
    public TMP_Text atkspeedText;
    public TMP_Text projcountText;
    public TMP_Text projspeedText;
    public TMP_Text projrangeText;

    public TMP_Text hpText;
    public TMP_Text speedText;


    // Buttons
    public Button hpUpgradeButton;
    public TMP_Text hpCost;

    public Button speedUpgradeButton;
    public TMP_Text speedCost;

    public Button damageUpgradeButton;
    public TMP_Text damageCost;

    public Button atkspeedUpgradeButton;
    public TMP_Text atkspeedCost;

    public TMP_Text projcountcost;
    public TMP_Text projspeedcost;
    public TMP_Text projrangecost;

    public Button respawnButton;

    public Button unlockAscensionButton;
    public TMP_Text ascensionCost;
    public GameObject ascensionLock;
    public int ascensionPrice = 300_000;

    private void Start()
    {
        UpdateSoulsText();
        UpdateHPText();
        UpdateSpeedText();
        UpdateDamageText();
        UpdateAtkspeedText();
        UpdateProjCountText();
        UpdateProjSpeedText();
        UpdateProjRangeText();
        UpdateAscensionScreen();
    }

    public void UpdateSoulsText()
    {
        soulsText.text = $"Souls: {GameManager.Instance.playerSouls}";
    }
    void UpdateHPText()
    {
        hpText.text = GameManager.Instance.psd.maxhp.ToString();
        hpCost.text = GameManager.Instance.psd.hpUpgradeCost.ToString();
    }
    void UpdateSpeedText()
    {
        speedText.text = GameManager.Instance.psd.speed.ToString();
        speedCost.text = GameManager.Instance.psd.speedUpgradeCost.ToString();
    }
    void UpdateDamageText()
    {
        damageText.text = GameManager.Instance.psd.damage.ToString();
        damageCost.text = GameManager.Instance.psd.damageUpgradeCost.ToString();
    }
    void UpdateAtkspeedText()
    {
        atkspeedText.text = GameManager.Instance.psd.atkspeed.ToString();
        atkspeedCost.text = GameManager.Instance.psd.atkspeedUpgradeCost.ToString();
    }
    void UpdateProjCountText()
    {
        projcountText.text = GameManager.Instance.psd.projcount.ToString();
        projcountcost.text = GameManager.Instance.psd.projcountCost.ToString();
    }
    void UpdateProjSpeedText()
    {
        projspeedText.text = GameManager.Instance.psd.projspeed.ToString();
        projspeedcost.text = GameManager.Instance.psd.projspeedCost.ToString();
    }
    void UpdateProjRangeText()
    {
        projrangeText.text = GameManager.Instance.psd.projrange.ToString();
        projrangecost.text = GameManager.Instance.psd.projrangeCost.ToString();
    }
    void UpdateAscensionScreen()
    {
        if (GameManager.Instance.ascended)
        {
            ascensionLock.SetActive(false);
        }
    }
    public void UpgradeHP()
    {
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
    public void UpgradeSpeed()
    {
        if (GameManager.Instance.playerSouls >= GameManager.Instance.psd.speedUpgradeCost)
        {
            GameManager.Instance.playerSouls -= GameManager.Instance.psd.speedUpgradeCost;
            GameManager.Instance.psd.UpgradeSpeed();
            UpdateSpeedText();
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls!");
        }
    }
    public void UpgradeDamage()
    {
        Debug.Log("Button pressed");
        if (GameManager.Instance.playerSouls >= GameManager.Instance.psd.damageUpgradeCost)
        {
            GameManager.Instance.playerSouls -= GameManager.Instance.psd.damageUpgradeCost;
            GameManager.Instance.psd.UpgradeDamage();
            UpdateDamageText();
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls!");
        }
    }
    public void UpgradeAttackSpeed()
    {
        if (GameManager.Instance.playerSouls >= GameManager.Instance.psd.atkspeedUpgradeCost)
        {
            GameManager.Instance.playerSouls -= GameManager.Instance.psd.atkspeedUpgradeCost;
            GameManager.Instance.psd.UpgradeAtkspeed();
            UpdateAtkspeedText();
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls!");
        }
    }
    public void UpgradeProjcount()
    {
        if (GameManager.Instance.playerSouls >= GameManager.Instance.psd.projcountCost)
        {
            GameManager.Instance.playerSouls -= GameManager.Instance.psd.projcountCost;
            GameManager.Instance.psd.UpgradeProjcount();
            UpdateProjCountText();
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls");
        }
    }
    public void UpgradeProjspeed()
    {
        if (GameManager.Instance.playerSouls >= GameManager.Instance.psd.projspeedCost)
        {
            GameManager.Instance.playerSouls -= GameManager.Instance.psd.projspeedCost;
            GameManager.Instance.psd.UpgradeProjspeed();
            UpdateProjSpeedText();
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls");
        }
    }
    public void UpgradeProjrange()
    {
        if (GameManager.Instance.playerSouls >= GameManager.Instance.psd.projrangeCost)
        {
            GameManager.Instance.playerSouls -= GameManager.Instance.psd.projrangeCost;
            GameManager.Instance.psd.UpgradeProjrange();
            UpdateProjRangeText();
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls");
        }
    }
    public void UnlockAscension()
    {
        if (GameManager.Instance.playerSouls >= ascensionPrice)
        {
            GameManager.Instance.playerSouls -= ascensionPrice;
            GameManager.Instance.ascended = true;
            UpdateAscensionScreen();
        }
        else
        {
            Debug.Log("Not enough souls!");
        }
    }
    public void TestButton()
    {
        Debug.Log("Test button");
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
