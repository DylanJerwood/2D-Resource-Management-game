using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinLoseManager : MonoBehaviour {
    public TMP_Text WLtext;
    public GameObject canvas;

    public WaveUI WaveUI;

    private void Awake() {
        canvas.SetActive(false);
    }

    private void Update() {
        if(GameObject.FindGameObjectsWithTag("Core").Length == 0) {
            WLtext.text = "YOU LOSE!!";
            canvas.SetActive(true);
        }
        if(WaveUI.wavesFinished == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            WLtext.text = "YOU WIN!!";
            canvas.SetActive(true);
        }
    }
}
