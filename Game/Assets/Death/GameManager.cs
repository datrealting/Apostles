using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerSouls = 0; // Persistent souls
    public WeaponStats weaponStats;
    public DangerState dangerState;

    // Music variables
    public AudioSource bass1;
    public AudioSource bass2;
    public AudioSource dr1;
    public AudioSource dr2;
    public AudioSource dr3;
    public AudioSource g1;
    public AudioSource g2;

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
        HandleMusic(dangerState);
    }
    private void HandleMusic(DangerState dangerState)
    {
        switch (dangerState) 
        {
            case DangerState.Low:
                bass1.volume = 1;
                dr1.volume = 1;
                break;
            case DangerState.Medium:
                bass1.volume = 1;
                dr1.volume = 1;
                bass2.volume = 1;
                dr2.volume = 1;
                break;
            case DangerState.High:
                bass1.volume = 1;
                dr1.volume = 1;
                bass2.volume = 1;
                dr2.volume = 1;
                dr3.volume = 1;
                g1.volume = 1;
                g2.volume = 1;
                break;
        }
    }
    public enum DangerState
    {
        Low,
        Medium,
        High
    }
}