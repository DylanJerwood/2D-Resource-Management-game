using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    public GameObject GBS;
    private GridBuildingSystem gridBuildingSystem;

    public GameObject ghost;
    private BuildingGhost buildingGhost;

    public List<GameObject> menuList;

    private int menuNum;

    // Start is called before the first frame update
    void Start()
    {
        gridBuildingSystem = GBS.GetComponent<GridBuildingSystem>();
        buildingGhost = ghost.GetComponent<BuildingGhost>();
    }

    public void DrillMenuButon()
    {
        menuNum = 0;

        gridBuildingSystem.placedObjectTypeSO = null;  
        gridBuildingSystem.placingObject = false;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  

        foreach (GameObject menu in menuList) {
            if (menu != menuList[menuNum]) {
                menu.SetActive(false);
            }
        }

        if(menuList[menuNum].activeInHierarchy) {
            menuList[menuNum].SetActive(false);
        }
        else{
            menuList[menuNum].SetActive(true);
        }
        
    }
    public void ConveyorMenuButton()
    {
        menuNum = 1;

        gridBuildingSystem.placedObjectTypeSO = null;  
        gridBuildingSystem.placingObject = false;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        
        foreach (GameObject menu in menuList) {
            if (menu != menuList[menuNum]) {
                menu.SetActive(false);
            }
        }
        if(menuList[menuNum].activeInHierarchy) {
            menuList[menuNum].SetActive(false);
        }
        else{
            menuList[menuNum].SetActive(true);
        }
        
    }
    public void DrillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[0];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[0];  
        buildingGhost.createPlacementIndicator = true;
    }
    public void BigdrillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[1];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[1];  
        buildingGhost.createPlacementIndicator = true;
    }    
    public void LongdrillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[2];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[2];  
        buildingGhost.createPlacementIndicator = true;
    }
    public void ConveyorButton()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[3];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[3];  
        buildingGhost.createPlacementIndicator = true;
    }
}
