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

    private void Awake() {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        wavesFinished = false; 
    }

    private void Update() {
        enemiesLeft.text = "Enemies: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(GameObject.FindGameObjectsWithTag("WaveManager").Length == 1) {
            wavesLeft.text = "Wave: " + waveManager.wavesPassed.ToString() + "/" + waveManager.numOfWaves.ToString();
            float timeTillNextWaves = waveManager.nextSpawnTime - Time.timeSinceLevelLoad;
            if(waveManager.numOfWaves > waveManager.wavesPassed) {
                timeTillNextWave.text = string.Format("{0:#.00}", timeTillNextWaves);
            }
        }
        else {
            wavesFinished = true;
        }
    }
    
    private void SkipToNextWave(){
        waveManager.nextSpawnTime = Time.timeSinceLevelLoad;
    }
}
