using UnityEngine;

public class DEMOPLEASEDELETElootboxscreen : MonoBehaviour
{
    public LootboxScript lootbox;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            lootbox.ToggleLootboxScreen();
        }
    }
}
