using System.Runtime.CompilerServices;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSE : MonoBehaviour, SEInterface
{
    public virtual OverrideType overrideType { get; }

    private bool initialised = false;
    protected float _duration = 5f;
    public virtual float duration
    {
        get => _duration;
        set => _duration = value; // Allow setting it in derived classes
    }

    public virtual float tickFrequency => 1f; // number of seconds between each tick (e.g., 0.5 for 2 ticks per second)

    protected float elapsed = 0f;
    protected float elapsedLastTick = 0f;   // time since last tick
    protected NPCStats targetStats;

    public void Initialise(GameObject target)
    {
        targetStats = target.GetComponent<NPCStats>();
        if (targetStats == null)
        {
            Debug.Log("I've fucked up sorry");
            Destroy(this);
        }

        BaseSE[] existingEffects = target.GetComponents<BaseSE>();
        switch (overrideType)
        {
            case OverrideType.Extend:
                Debug.Log("TYPE: EXTEND");
                foreach (BaseSE effect in existingEffects)
                {
                    if (effect.GetType() == this.GetType())
                    { 
                        if (effect.initialised)
                        {
                            effect.IncreaseDuration(this.duration); // Extend the duration
                            Destroy(this);
                        }
                        else
                        {
                            OnApply();
                        }
                    }
                }
                break;

            case OverrideType.Refresh:
                Debug.Log("TYPE: REFRESH");
                foreach (BaseSE effect in existingEffects)
                {
                    if (effect.GetType() == this.GetType())
                    {
                        if (effect.initialised)
                        {
                            effect.elapsed = 0f;  // Extend the duration
                            Destroy(this);
                        }
                        else
                        {
                            OnApply();
                        }         
                    }
                }
                break;

            case OverrideType.Stack:
                Debug.Log("TYPE: STACK");
                // Stack the effect (apply another one)
                OnApply();
                break;

            case OverrideType.Nothing:
                Debug.Log("TYPE: NOTHING");
                // Destroy this effect if no action is needed
                Destroy(this);
                break;
        }
        this.initialised = true;
    }

    public void IncreaseDuration(float duration)
    {
        this.duration += duration;
    }

    public abstract void OnApply();
    public abstract void OnTick();
    public abstract void OnRemoval();

    // if you ever need to cleanse something, this is (probably) the function you want (e.g. GetComponent<Burning>().RemoveEffect())
    public void RemoveEffect()
    {
        OnRemoval();
        Destroy(this);
    }

    void Update()
    {
        if (targetStats == null) return;
        elapsed += Time.deltaTime;
        elapsedLastTick += Time.deltaTime;
        if (elapsedLastTick >= tickFrequency)
        {
            OnTick();
            elapsedLastTick = 0f;
        }
        if (elapsed >= duration)
        {
            RemoveEffect();
        }

    }
}
