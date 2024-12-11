using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public string vName;
    public string description;
    public GameObject owner;
    public abstract void Apply(GameObject Target);
}
