using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BuildingGhost : MonoBehaviour
{
    //References the gridBuildingSystem script
    public GameObject GBS;
    private GridBuildingSystem gridBuildingSystem;

    //creates a list of all of the visual prefabs
    public List<GameObject> visualsList;

    //Variables
    public GameObject visual;
    public bool createPlacementIndicator;
    private GameObject indicator;
    private GameObject middleMan;
    
    //Start Script
    void Awake()
    {
        //calls gridBuildingSystem Script and stores it in a variable
        gridBuildingSystem = GBS.GetComponent<GridBuildingSystem>();
    }


    void Update()
    {
        //Checks is a building has been selected and that the player is placing an object
        if (gridBuildingSystem.placedObjectTypeSO != null && gridBuildingSystem.placingObject == true) {
            //Checks to see if a placement indicator has already been made
            if(createPlacementIndicator == true) {
                //If one has not been made it Instantiates one 
                middleMan = Instantiate(visual, new Vector3(0, 0, 0), Quaternion.identity);
                indicator = middleMan;

                //changes bool so you only make 1 indicator
                createPlacementIndicator = false;
            }
        }

        //Checks to see if you press right click (if you do if then removes the indicator object and stores null in the variable)
        if (Input.GetMouseButtonDown(1)) {
            visual = null;
            Destroy(indicator);
        }

        //Player selects building using 1-4    checks for already existing indicator and destroys it
        if (Input.GetKeyDown(KeyCode.Alpha1)) {if (indicator != null) {Destroy(indicator);} visual = visualsList[0]; createPlacementIndicator = true; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {if (indicator != null) {Destroy(indicator);} visual = visualsList[1]; createPlacementIndicator = true; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {if (indicator != null) {Destroy(indicator);} visual = visualsList[2]; createPlacementIndicator = true; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {if (indicator != null) {Destroy(indicator);} visual = visualsList[3]; createPlacementIndicator = true; }
    }

    //indicator movement
    void LateUpdate() {
        //checks if building is selected
        if(indicator != null) {
            //moves indicator to your mouse location thats snapped to the grid
            indicator.transform.position = gridBuildingSystem.GetMouseWorldSnappedPosition();

            //When you rotate the building this also rotates the indicator so you can see
            indicator.transform.rotation = Quaternion.Lerp(indicator.transform.rotation, gridBuildingSystem.GetPlacedObjectRotation(), Time.deltaTime * 15f);
        }
        
    }
}
