using System.Linq;
using UnityEngine;

public class PlayerRelicsHeld : MonoBehaviour
{
    public ItemEffect[] relics;

    public void AddRelic(ItemEffect relic)
    {
        relics.Append(relic);       // Append a unique relic to the list
        Debug.Log("Added relic: " + relic.vName + " || " + relic.description);
    }
    public void RemoveRelic(ItemEffect relic)
    {
        // not working until i know how we wanna work with multiple relics
    }
}
