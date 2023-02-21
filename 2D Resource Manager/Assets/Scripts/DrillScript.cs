using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillScript : MonoBehaviour
{
    //String that changes between each drill to use as a check for which one its on
    public string drillType;
    //GameObjects used for the OverlapBox feature
    public GameObject origin;
    public GameObject box;
    //Variables used for storing the conveyors around the drill
    private Conveyor[] conveyorList;
    private Conveyor conveyor;
    //variables used for the item the drill is giving out
    public ConveyorItem drillItem;
    private ConveyorItem item;
    private GameObject itemPosition;
    private Vector3 position;
    //variables used for loops and checks
    private int i = 0;
    private bool freeSpace = false;
    //variables to manage time
    private float nextItemDelivery = 0.0f;
    public float drillSpeed;

    private void Update() {
        //gathers List of all the conveyors in a square around the drill
        conveyorList = GetConveyorList();

        //Changes the conveyor the drill will place the item on to the next conveyor in the list
        if(Time.time > nextItemDelivery) {
            nextItemDelivery += drillSpeed;
            if(i < conveyorList.Length) {
                conveyor = conveyorList[i];
                i++;
            }
            else {
                i = 0;
                conveyor = conveyorList[i];
            }

            freeSpace = false;
            //Checks if there is a conveyor in the spot and if it already has an item on it...
            if(conveyor != null && conveyor.isSpaceTaken == false && conveyor.conveyorItem == null) {
                freeSpace = true; //...If so then it is a freee space
            }
            //if that spot doesnt isnt available...
            else {
                //...then it goes through the whole list to check the next spot thats available if there is one at all
                for(int y=0; y < conveyorList.Length; y++) {
                    i++;
                    if(i < conveyorList.Length) {
                        conveyor = conveyorList[i];
                        i++;
                    }
                    else {
                        i = 0;
                        conveyor = conveyorList[i];
                    }
                    if(conveyor != null && conveyor.isSpaceTaken == false && conveyor.conveyorItem == null) {
                        freeSpace = true;
                        break;
                    }
                    freeSpace = false;
                }
            }
            //If there has been a free space detected then it will palce an item on the conveyor and change its item variable to the item it placed on the conveyor
            if(freeSpace == true) {
                itemPosition = conveyor.transform.GetChild(1).gameObject;
                position = new Vector3(itemPosition.transform.position.x, itemPosition.transform.position.y, itemPosition.transform.position.z);
                item =  Instantiate(drillItem, position, Quaternion.identity);
                conveyor.conveyorItem = item;
                item.transform.parent = conveyor.transform;
            }
        }
    }

    //gets the list of all the conveyors around the drill using OverlapBox
    private Conveyor[] GetConveyorList() {
    
        //variables for the conveyor and list of conveyors
        Collider2D[] listOfConveyorsColliders = new Collider2D[12];
        Conveyor[] listOfConveyors = new Conveyor[12];
        //Variables for the OverlapBox function
        Vector2 boxScale = box.transform.localScale / 2;
        ContactFilter2D contactFilter = new ContactFilter2D();

        int i=0;
        //Checks which type of drill it is and changes the size of the list to the correct size
        if(drillType == "bigDrill") {
            listOfConveyorsColliders = new Collider2D[20];
            listOfConveyors = new Conveyor[20];
        }
        else if (drillType == "longDrill"){
            listOfConveyorsColliders = new Collider2D[20];
            listOfConveyors = new Conveyor[20];
        }
        //OverlapBox command returns an intager with the numver of colliders in the box, and populates a list called listOfConveyorsColliders with those colliders
        Physics2D.OverlapBox(origin.transform.position, boxScale, 0f, contactFilter, listOfConveyorsColliders);

        //it then converts the clist of colliders into a new list of GameObjects so i can use them for other tasks
        foreach(Collider2D col in listOfConveyorsColliders) {
            if(col) {
                listOfConveyors[i] = col.GetComponent<Collider2D>().GetComponent<Conveyor>();
            }

            i++;
        }

        return listOfConveyors;
    }

}
