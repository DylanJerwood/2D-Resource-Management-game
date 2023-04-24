using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedUI : MonoBehaviour {
    public Image speed1;
    public Image speed2;
    public Image speed3;
    public Image speed4;

    private playerMovement playerMovement;

    private void Start() {
        playerMovement = GameObject.Find("Player").GetComponent<playerMovement>();
    }

    private void Update() {
        if(playerMovement.currentGameSpeedSetting == 1) {
            speed1.color = new Color(speed1.color.r, speed1.color.g, speed1.color.b, 1f);
            speed2.color = new Color(speed1.color.r, speed1.color.g, speed1.color.b, .33f);
            speed3.color = new Color(speed1.color.r, speed1.color.g, speed1.color.b, .33f);
            speed4.color = new Color(speed1.color.r, speed1.color.g, speed1.color.b, .33f);
        }
        else if(playerMovement.currentGameSpeedSetting == 2) {
            speed2.color = new Color(speed1.color.r, speed1.color.g, speed1.color.b, 1f);
        }
        else if(playerMovement.currentGameSpeedSetting == 3) {
            speed3.color = new Color(speed1.color.r, speed1.color.g, speed1.color.b, 1f);
        }
        else if(playerMovement.currentGameSpeedSetting == 4) {
            speed4.color = new Color(speed1.color.r, speed1.color.g, speed1.color.b, 1f);
        }
    }
}
