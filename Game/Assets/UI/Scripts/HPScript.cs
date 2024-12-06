using UnityEngine;
using TMPro;

public class HPScript : MonoBehaviour
{
    public GameObject player;

    public TMP_Text text;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        text.SetText("Hearts: " + player.GetComponent<PlayerControl>().currenthp.ToString() + " / " + player.GetComponent<PlayerControl>().maxhp.ToString());
    }
}
