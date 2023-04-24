using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour {
    public TMP_Text wavesLeft;
    public TMP_Text timeTillNextWave;
    public TMP_Text enemiesLeft;
    public bool wavesFinished;
    private WaveManager waveManager;

    private void Start() {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        wavesFinished = false; 
    }

    private void Update() {
        
        if(GameObject.FindGameObjectsWithTag("WaveManager").Length == 1) {
            wavesLeft.text = "Wave: " + waveManager.wavesPassed.ToString() + "/" + waveManager.numOfWaves.ToString();
            enemiesLeft.text = "Enemies: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
            float timeTillNextWaves = waveManager.nextSpawnTime - Time.time;
            if(waveManager.numOfWaves > waveManager.wavesPassed) {
                timeTillNextWave.text = string.Format("{0:#.00}", timeTillNextWaves);
                wavesFinished = true;
            }
        }
    }
}