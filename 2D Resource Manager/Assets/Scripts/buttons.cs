using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    public GameObject GBS;
    private GridBuildingSystem gridBuildingSystem;

    public GameObject ghost;
    private BuildingGhost buildingGhost;

    // Start is called before the first frame update
    void Start()
    {
        gridBuildingSystem = GBS.GetComponent<GridBuildingSystem>();
        buildingGhost = ghost.GetComponent<BuildingGhost>();
    }

    public void drillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[0];
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = buildingGhost.visualsList[0];
        buildingGhost.createPlacementIndicator = true;
    }
    public void BigdrillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[1];
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = buildingGhost.visualsList[1];
        buildingGhost.createPlacementIndicator = true;
    }    
    public void LongdrillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[2];
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = buildingGhost.visualsList[2];
        buildingGhost.createPlacementIndicator = true;
    }
        public void ConveyorButton()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[3];
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = buildingGhost.visualsList[3];
        buildingGhost.createPlacementIndicator = true;
    }
}
