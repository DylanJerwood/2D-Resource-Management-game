using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    public int numOfWaves;
    public int difficulty;
    public float timeBetweenWaves;
    public GameObject enemy;

    public int wavesPassed;
    public float nextSpawnTime;
    private Vector2 spawnOrigin;

    private void Awake() {
        spawnOrigin = GameObject.FindGameObjectWithTag("EnemySpawn").transform.position;
        nextSpawnTime = timeBetweenWaves;
    }

    private void Update() {
        if(Time.time > nextSpawnTime) {
            if(wavesPassed < numOfWaves) {
                for (int i = 0; i < NumOfEnemiesSpawned(); i++) {
                    Vector2 spawnLocation = spawnOrigin + Random.insideUnitCircle * 10;
                    // Debug.Log(spawnLocation);
                    Instantiate(enemy, spawnLocation, Quaternion.identity);
                }
                nextSpawnTime += timeBetweenWaves;
                wavesPassed++;
            }
            else{
                Destroy(gameObject);
            }
        }
    }

    private int NumOfEnemiesSpawned() {
        if (difficulty == 0) {
            return 2;
        }
        if (difficulty == 1) {
            return 4;
        }
        if (difficulty == 2) {
            return 6;
        }
        if (difficulty == 3) {
            return 8;
        }
        if (difficulty == 4) {
            return 10;
        }
        if (difficulty == 5) {
            return 12;
        }
        return 100;
    }
}

