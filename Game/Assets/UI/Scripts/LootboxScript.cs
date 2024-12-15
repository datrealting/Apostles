using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LootboxScript : MonoBehaviour
{
    protected GameManager gm;
    protected RelicHandlerScript rs;

    public GameObject screen;
    public GameObject card;
    public GameObject cardholder;
    protected bool screenon;

    public int itemsDropped = 3;
    protected Color common;
    protected Color uncommon;
    protected Color rare;
    protected Color epic;
    protected Color legendary;


    private void Awake()
    {
        gm = GameObject.Find("GameController").GetComponent<GameManager>();
        rs = gm.GetComponent<RelicHandlerScript>();
        screen = GameObject.Find("LootboxScreen");
        screen.SetActive(false);

        common = new Color(133, 133, 133);
        uncommon = new Color(0, 255, 110);
        rare = new Color(0, 85, 255);
        epic = new Color(119, 3, 252);
        legendary = new Color(252, 186, 3);
    }
    protected void GenerateCard()
    {
        // Generate random relic
        GameObject relic = rs.GetRandomDrop();
        Debug.Log("Dropped relic: " + relic);

        // Visuals
        Transform itemButtonTransform = card.transform.Find("Fill/ItemButton");
        itemButtonTransform.GetComponent<Image>().sprite = relic.GetComponent<SpriteRenderer>().sprite;

        Transform itemName = card.transform.Find("Fill/Name");
        itemName.GetComponent<TextMeshPro>().text = relic.GetComponent<Item>().itemEffect.vName;

        Transform itemRarityOutline = card.transform.Find("Fill/Outline");

        switch (relic.GetComponent<ItemEffect>().rarity)
        {
            case Enum_Rarity.Common:
                itemRarityOutline.GetComponent<Image>().color = common;
                break;

            case Enum_Rarity.Uncommon:
                itemRarityOutline.GetComponent<Image>().color = uncommon;
                break;

            case Enum_Rarity.Rare:
                itemRarityOutline.GetComponent<Image>().color = rare;
                break;

            case Enum_Rarity.Epic:
                itemRarityOutline.GetComponent<Image>().color = epic;
                break;

            case Enum_Rarity.Legendary:
                itemRarityOutline.GetComponent<Image>().color = legendary;
                break;
        }

        Instantiate(card, cardholder.transform);
    }
    protected void GenerateAllRelicCards()
    {
        Debug.Log(cardholder.transform.childCount);
        while (cardholder.transform.childCount > 0)
        {
            DestroyImmediate(cardholder.transform.GetChild(0).gameObject);
        }
        for (int i = 0; i < itemsDropped; i++)
        {
            GenerateCard();
        }
    }
    public void ToggleLootboxScreen()
    {
        if (!screenon && !gm.pauseLocked)
        {
            screenon = true;
            Time.timeScale = 0;
            gm.gamePaused = true;
            GenerateAllRelicCards();
            screen.SetActive(true);
        }
        else if (!screenon && gm.pauseLocked)
        {
            // DONT DO ANYTHING
        }
        else
        {
            screenon = false;
            Time.timeScale = 1;
            gm.gamePaused = false;
            screen.SetActive(false);
        }
    }
    public static Transform FindChildRecursive(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
            Transform result = FindChildRecursive(child, childName);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
}
