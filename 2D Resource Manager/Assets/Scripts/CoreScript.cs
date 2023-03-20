using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour {

    private GridBuildingSystem gridBuildingSystem;
    
    private void Start() {
        gridBuildingSystem = GameObject.Find("GridBuildingSystem").GetComponent<GridBuildingSystem>();
    }
}

