using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour {
    //Variable for the grid after its created
    public Pathfinding pathfinding;
    //variable to create a new path, will be used when the surrounding objects change
    public bool setPath;

    private void Start() {
        //Creates the grid
       pathfinding = new Pathfinding(200,200);
        //Tells the pathfinding agents to calculate a path
       setPath = true;
    } 

    private void Update() {
        //If set path is true
        if(setPath == true) {
            //It finds all the GameObjects with the tag "Enemy"
            GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
            //Then for every GameObject in the list of enemies...
            foreach(GameObject enemy in enemies) {
                //...it gets the enemy script...
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                //...and changes its set path to true, which will then re-calculate a path in the enmy script
                enemyScript.setPath = true;
            }
            //And finnaly it changes setPath to false so it doesnt constantly calculate paths
            setPath = false;
        }
    }
}
