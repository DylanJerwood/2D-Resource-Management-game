using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public GameObject GBS;
    private GridBuildingSystem gridBuildingSystem;

    public GameObject ghost;
    private BuildingGhost buildingGhost;

    public List<GameObject> menuList;

    private int menuNum;


    private void Start()
    {
        gridBuildingSystem = GBS.GetComponent<GridBuildingSystem>();
        buildingGhost = ghost.GetComponent<BuildingGhost>();
    }

    private void DrillMenuButon()
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
    private void ConveyorMenuButton()
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
    private void TurretMenuButton()
    {
        menuNum = 2;

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
    private void WallMenuButton()
    {
        menuNum = 3;

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
    private void DrillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[0];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[0];  
        buildingGhost.createPlacementIndicator = true;
    }
    private void BigdrillButon()
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
    private void ConveyorButton()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[3];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[3];  
        buildingGhost.createPlacementIndicator = true;
    }
    private void TurretButton()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[4];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[4];  
        buildingGhost.createPlacementIndicator = true;
    }
    private void WallButton()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[5];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[5];  
        buildingGhost.createPlacementIndicator = true;
    }
}

