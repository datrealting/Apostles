using UnityEngine;
using TMPro;
using UnityEditor.ShaderGraph;

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
        text.SetText("Hearts: " + player.GetComponent<PlayerStats>().currenthp.ToString() + " / " + player.GetComponent<PlayerStats>().maxhp.ToString());
        text.color = Color.green;
    }
}
