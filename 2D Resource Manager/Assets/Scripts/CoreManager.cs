using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : MonoBehaviour{

    public PlacedObjectTypeSO core;
    public bool placeCore = false;
    private GridBuildingSystem gridBuildingSystem;

    private void Start() {
        gridBuildingSystem = GameObject.Find("GridBuildingSystem").GetComponent<GridBuildingSystem>();
        
    }

    private void Update() {
        if(placeCore == true) {
            gridBuildingSystem.PlaceObjectOnAwake(core, new Vector3(-1,-1,0));
            placeCore = false;         
        }
    }
}
