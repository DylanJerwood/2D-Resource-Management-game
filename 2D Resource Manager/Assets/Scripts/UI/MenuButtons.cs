using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject GBS;
    private GridBuildingSystem gridBuildingSystem;

    public GameObject ghost;
    private BuildingGhost buildingGhost;

    public GameObject ControlsWindow;

    public List<GameObject> menuList;


    private void Awake()
    {
        gridBuildingSystem = GBS.GetComponent<GridBuildingSystem>();
        buildingGhost = ghost.GetComponent<BuildingGhost>();
    }

    private void DrillMenuButon()
    {
        int menuNum = 0;

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
        int menuNum = 1;

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
        int menuNum = 2;

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
        int menuNum = 3;

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
        int buttonNum = 1;
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[buttonNum];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[buttonNum];  
        buildingGhost.createPlacementIndicator = true;
    }
    private void BigdrillButon()
    {
        int buttonNum = 2;
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[buttonNum];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[buttonNum];  
        buildingGhost.createPlacementIndicator = true;
    }    
    private void ConveyorButton()
    {
        int buttonNum = 3;
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[buttonNum];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[buttonNum];  
        buildingGhost.createPlacementIndicator = true;
    }
    private void TurretButton()
    {
        int buttonNum = 4;
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[buttonNum];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[buttonNum];  
        buildingGhost.createPlacementIndicator = true;
    }
    private void WallButton()
    {
        int buttonNum = 5;
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[buttonNum];  
        gridBuildingSystem.placingObject = true;
        buildingGhost.visual = null;  
        Destroy(buildingGhost.indicator);  
        buildingGhost.visual = buildingGhost.visualsList[buttonNum];  
        buildingGhost.createPlacementIndicator = true;
    }
    private void ResetButton()
    {
        SceneManager.LoadScene(0);
    }
    private void ControlsWindowButton() {
        if(ControlsWindow.activeInHierarchy) {
            ControlsWindow.SetActive(false);
        }
        else if(!ControlsWindow.activeInHierarchy) {
            ControlsWindow.SetActive(true);
        }
    }
}

