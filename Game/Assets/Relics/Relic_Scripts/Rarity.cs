using UnityEngine;

[CreateAssetMenu(fileName = "New Rarity", menuName = "Items/Rarity")]
public class Rarity : ScriptableObject
{
    public string rarityName;      // E.g., "Common", "Rare"
    public Color rarityColor;      // Color to represent this rarity
    [Range(0, 1)]                  // Ensures drop chances are between 0 and 1
    public float dropChance;       // Probability for this rarity
}
