using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gamePaused = false;
    public bool pauseLocked = false;    // locks the screen such that it can't be double paused etc. 
    public GameObject menuscreen;
    public GameObject sureScreen;

    public int playerSouls = 0; // Persistent souls
    public PlayerStatData psd;
    public GameObject player;
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

    // Cheat variables
    private int soulcheatincrease = 30000;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            weaponStats = new WeaponStats();
            psd = new PlayerStatData();
            DontDestroyOnLoad(gameObject); // Keeps this object across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
        player = GameObject.Find("Player");
        HandleMusic(dangerState);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddSouls(soulcheatincrease);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused && !pauseLocked)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        menuscreen.SetActive(true);
        gamePaused = true;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        menuscreen.SetActive(false);
        gamePaused = false; 
    }

    public void Resume()
    {
        Unpause();
    }
    public void MainMenuQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void DesktopQuit()
    {
        sureScreen.SetActive(true);
    }
    public void NotSure()
    {
        sureScreen.SetActive(false);
    }
    public void Sure()
    {
        Application.Quit();
    }

    // AddSouls() takes the souls the enemy usually drops, multiplies it with the player's multipliers etc. and 
    public int AddSouls(float amount)
    {
        if (player != null)
        {
            float mult = player.GetComponent<PlayerControl>().soulDropMult;
            this.playerSouls += Mathf.RoundToInt(amount * mult);
            return Mathf.RoundToInt(amount * mult);
        }
        else
        {
            this.playerSouls += Mathf.RoundToInt(amount);
            return Mathf.RoundToInt(amount);
        }
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