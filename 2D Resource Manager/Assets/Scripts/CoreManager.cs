using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : MonoBehaviour{

    public PlacedObjectTypeSO core;
    public bool placeCore = false;
    public Vector3 placementLocation;
    private GridBuildingSystem gridBuildingSystem;

    private void Start() {
        gridBuildingSystem = GameObject.Find("GridBuildingSystem").GetComponent<GridBuildingSystem>();
        gridBuildingSystem.PlaceObjectOnAwake(core, placementLocation);
    }

    private void Update() {
        
    }
}
