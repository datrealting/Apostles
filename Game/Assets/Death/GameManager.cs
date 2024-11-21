using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerSouls = 0; // Persistent souls
    public WeaponStats weaponStats;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            weaponStats = new WeaponStats();
            DontDestroyOnLoad(gameObject); // Keeps this object across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }
}