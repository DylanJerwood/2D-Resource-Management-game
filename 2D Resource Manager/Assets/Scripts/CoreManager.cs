using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : MonoBehaviour{

    public PlacedObjectTypeSO core;
    public bool placeCore = false;
    private GridBuildingSystem gridBuildingSystem;

    private void Start() {
        gridBuildingSystem = GameObject.Find("GridBuildingSystem").GetComponent<GridBuildingSystem>();
        gridBuildingSystem.PlaceObjectOnAwake(core, new Vector3(-1,-1,0));
    }

    private void Update() {
        
    }
}
