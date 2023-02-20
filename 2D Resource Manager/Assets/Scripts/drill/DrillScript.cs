using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillScript : MonoBehaviour
{
    public string drillType;
    public GameObject origin;
    public GameObject box;
    public Conveyor[] conveyorList;
    public Conveyor conveyor;
    public ConveyorItem drillItem;
    private ConveyorItem x;
    private GameObject itemPosition;
    private Vector3 position;

    private float nextItemDelivery = 0.0f;
    public float drillSpeed = 0.5f;

    private void Start() {
        conveyorList = GetConveyorList();
    }

    private void Update() {

        conveyorList = GetConveyorList();
        

        
        foreach(var con in conveyorList) {
            conveyor = con;
            if(Time.time > nextItemDelivery) {
                nextItemDelivery += drillSpeed;
                if(conveyor != null) {
                    if(conveyor.conveyorItem == null) {
                        itemPosition = conveyor.transform.GetChild(1).gameObject;
                        position = new Vector3(itemPosition.transform.position.x, itemPosition.transform.position.y, itemPosition.transform.position.z);
                        x =  Instantiate(drillItem, position, Quaternion.identity);
                        conveyor.conveyorItem = x;
                        x.transform.parent = conveyor.transform;
                    }
                }
            }
        }
    }

    private Conveyor[] GetConveyorList() {
    
        Collider2D[] listOfConveyorsColliders = new Collider2D[12];
        Conveyor[] listOfConveyors = new Conveyor[12];
        Vector2 boxScale = box.transform.localScale / 2;
        
        ContactFilter2D contactFilter = new ContactFilter2D();
        int i=0;
        
        if(drillType == "bigDrill") {
            listOfConveyorsColliders = new Collider2D[20];
            listOfConveyors = new Conveyor[20];
        }
        else if (drillType == "longDrill"){
            listOfConveyorsColliders = new Collider2D[20];
            listOfConveyors = new Conveyor[20];
        }
        
        int numOfConveyors = Physics2D.OverlapBox(origin.transform.position, boxScale, 0f, contactFilter, listOfConveyorsColliders);


        foreach(var col in listOfConveyorsColliders) {
            if(col) {
                listOfConveyors[i] = col.GetComponent<Collider2D>().GetComponent<Conveyor>();
            }

            i++;
        }

        return listOfConveyors;
    }

}
