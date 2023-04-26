using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreScript : MonoBehaviour {

    private GridBuildingSystem gridBuildingSystem;
    private MaterialManager materialManager;
    
    private void Awake() {
        gridBuildingSystem = GameObject.Find("GridBuildingSystem").GetComponent<GridBuildingSystem>();
        materialManager = GameObject.Find("MaterialManager").GetComponent<MaterialManager>();
    }

    public void IncreaseMatCount(string incomeMat) {
        if(incomeMat == "Iron") {
            materialManager.ironCount = materialManager.ironCount + 1;
        }
        else if(incomeMat == "Copper") {
            materialManager.copperCount = materialManager.copperCount + 1;
        }
        else{
            Debug.Log("Name not recognized");
        }
    }
}

