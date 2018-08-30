using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    GameObject player;
    public Button attackButton1;
    public Button attackButton2;
    public Canvas playerDirectionCanvas;

    private void Start()
    {
        player = PlayerManager.instance.player;
        Text attackButton1Text = attackButton1.GetComponentInChildren<Text>();
        attackButton1Text.text = player.GetComponent<PlayerSkills>().combos[0][0].name;

        Text attackButton2Text = attackButton2.GetComponentInChildren<Text>();
        attackButton2Text.text = player.GetComponent<PlayerSkills>().combos[1][0].name;
    }

    private void Update()
    {
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        playerDirectionCanvas.transform.position = playerPosition;
    }
}
