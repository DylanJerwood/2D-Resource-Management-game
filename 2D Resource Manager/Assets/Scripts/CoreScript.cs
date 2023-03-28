using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour {

    public int ironCount;
    public int copperCount;
    private GridBuildingSystem gridBuildingSystem;
    
    private void Start() {
        gridBuildingSystem = GameObject.Find("GridBuildingSystem").GetComponent<GridBuildingSystem>();
    }

    private void Update() {

    }

    public void IncreaseMatCount(string incomeMat) {
        if(incomeMat == "Iron") {
            ironCount = ironCount + 1;
        }
        else if(incomeMat == "Copper") {
            copperCount = copperCount + 1;
        }
        else{
            Debug.Log("Name not recognized");
        }
    }
}

