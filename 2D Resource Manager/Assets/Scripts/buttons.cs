using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    public GameObject GBS;
    private GridBuildingSystem gridBuildingSystem;

    // Start is called before the first frame update
    void Start()
    {
        gridBuildingSystem = GBS.GetComponent<GridBuildingSystem>();
    }

    public void drillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[0];
    }
    public void BigdrillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[1];
    }    
    public void LongdrillButon()
    {
        gridBuildingSystem.placedObjectTypeSO = gridBuildingSystem.placedObjectTypeSOList[2];

    }
}
