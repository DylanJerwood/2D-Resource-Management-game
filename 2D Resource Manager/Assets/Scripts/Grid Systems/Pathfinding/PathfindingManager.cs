using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour {

    public Pathfinding pathfinding;
    public bool setPath;

    private void Start() {
       pathfinding = new Pathfinding(200,200);
       setPath = true;
    } 

    private void Update() {
        if(setPath == true) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

            foreach(GameObject enemy in enemies) {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.setPath = true;
            }
            setPath = false;
        }
    }
}
