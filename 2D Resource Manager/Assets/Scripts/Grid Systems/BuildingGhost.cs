using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BuildingGhost : MonoBehaviour
{
    public GameObject GBS;
    private GridBuildingSystem gridBuildingSystem;

    [SerializeField] public List<Transform> visualsList;
    public Transform visual;
    public bool createPlacementIndicator;
    private Transform indicator;
    private Transform middleMan;
    

    void Awake()
    {
        gridBuildingSystem = GBS.GetComponent<GridBuildingSystem>();
    }


    void Update()
    {
        if (gridBuildingSystem.placedObjectTypeSO != null && gridBuildingSystem.placingObject == true) {
            if(createPlacementIndicator == true) {
                middleMan = Instantiate(visual, new Vector3(0, 0, 0), Quaternion.identity);
                indicator = middleMan;
                Destroy(middleMan);

                createPlacementIndicator = false;
            }
            indicator.position = UtilsClass.GetMouseWorldPosition();
        }

        if (Input.GetMouseButtonDown(1)) {
            visual = null;
            Destroy(indicator);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {if (indicator != null) {Destroy(indicator);} visual = visualsList[0]; createPlacementIndicator = true; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {if (indicator != null) {Destroy(indicator);} visual = visualsList[1]; createPlacementIndicator = true; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {if (indicator != null) {Destroy(indicator);} visual = visualsList[2]; createPlacementIndicator = true; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {if (indicator != null) {Destroy(indicator);} visual = visualsList[3]; createPlacementIndicator = true; }
    }

    //what do i need to happen

    // i need the visuals position to move with the mouses position
    // i need to create a visual object the can follow the mouse
}
