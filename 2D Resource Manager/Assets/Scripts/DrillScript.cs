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
    public GameObject matBox;
    //Variables used for storing the conveyors around the drill
    private Conveyor[] conveyorList;
    private Conveyor conveyor;
    //variables used for the item the drill is giving out
    public ConveyorItem[] drillItem;
    private int drillItemIndex = 20;
    private ConveyorItem item;
    private GameObject itemPosition;
    private Vector3 position;
    //variables used for loops and checks
    private int i = 0;
    private bool freeSpace = false;
    //variables to manage time
    private float nextItemDelivery = 0.0f;
    public float timeTakenTillMine;
    private float drillSpeed;

    private void Awake() {
        DetectMaterial();
    }

    private void Update() {
        
        if(drillItemIndex != 20) {
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
                //if that spot isnt available...
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
                    item =  Instantiate(drillItem[drillItemIndex], position, Quaternion.identity);
                    item.name = drillItem[drillItemIndex].name;
                    conveyor.conveyorItem = item;
                    item.transform.parent = conveyor.transform;
                }
            }
        }
    }

    //gets the list of all the conveyors around the drill using OverlapBox
    private Conveyor[] GetConveyorList() {
    
        //variables for the conveyor and list of conveyors
        Collider2D[] listOfConveyorsColliders = new Collider2D[12];
        Conveyor[] listOfConveyors = new Conveyor[12];
        //Variables for the Overlapcircle function
        float boxScale = box.transform.localScale.magnitude;
        ContactFilter2D contactFilter = new ContactFilter2D();

        int i=0;
        //Checks which type of drill it is and changes the size of the list to the correct size
        if(drillType == "bigDrill") {
            listOfConveyorsColliders = new Collider2D[20];
            listOfConveyors = new Conveyor[20];
        }
        //OverlapBox command returns an intager with the numver of colliders in the box, and populates a list called listOfConveyorsColliders with those colliders
        Physics2D.OverlapCircle(origin.transform.position, boxScale, contactFilter, listOfConveyorsColliders);

        //it then converts the clist of colliders into a new list of GameObjects so i can use them for other tasks
        foreach(Collider2D col in listOfConveyorsColliders) {
            if(col) {
                listOfConveyors[i] = col.GetComponent<Collider2D>().GetComponent<Conveyor>();
            }

            i++;
        }

        return listOfConveyors;
    }
    //detects which material to mine based on whats its atop
    private void DetectMaterial() {

        List<Collider2D> listOfMat = new List<Collider2D>();
        
        Vector2 boxScale = matBox.transform.localScale / 2;
        ContactFilter2D contactFilter = new ContactFilter2D();

        Physics2D.OverlapBox(origin.transform.position, boxScale, 0f, contactFilter, listOfMat);
        int numOfIron = 0;
        int numOfCopper = 0;

        foreach(Collider2D col in listOfMat) {
            if(col){
                if(col.name == "IronVein") {
                    numOfIron = numOfIron + 1;
                }
                if(col.name == "CopperVein") {
                    numOfCopper = numOfCopper + 1;
                }
            }
        }
        if(numOfCopper >= numOfIron) {
            drillItemIndex = 1;
            drillSpeed = timeTakenTillMine / numOfCopper;
        }
        else{
            drillItemIndex = 0;
            drillSpeed = timeTakenTillMine / numOfIron;
        }
    }

    void OnDrawGizmosSelected() {
        float circleScale = box.transform.localScale.magnitude;
        Vector3 boxScale = matBox.transform.localScale / 2;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin.transform.position, circleScale);
        Gizmos.DrawWireCube(origin.transform.position, boxScale);
    }

}
