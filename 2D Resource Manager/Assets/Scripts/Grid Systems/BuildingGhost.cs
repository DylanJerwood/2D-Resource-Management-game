using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BuildingGhost : MonoBehaviour
{
    public GameObject GBS;
    private GridBuildingSystem gridBuildingSystem;

    [SerializeField] public List<GameObject> visualsList;
    public GameObject visual;
    public bool createPlacementIndicator;
    private GameObject indicator;
    private GameObject middleMan;
    

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

                createPlacementIndicator = false;
            }
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

    void LateUpdate() {
        if(indicator != null) {
            indicator.transform.position = gridBuildingSystem.GetMouseWorldSnappedPosition();

            indicator.transform.rotation = Quaternion.Lerp(indicator.transform.rotation, gridBuildingSystem.GetPlacedObjectRotation(), Time.deltaTime * 15f);
        }
        
    }

    //what do i need to happen

    //change rotation of indicator
    //change colour depending on weather they can build or not
}
