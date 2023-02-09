using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillScript : MonoBehaviour
{
    public string drillType;
    public List<Conveyor> conveyorList;
    public List<GameObject> raycastList;
    public Conveyor conveyor;

    private void Start() {

    }

    private void Update() {

        GetConveyorList();
        
    }

    private List<Conveyor> GetConveyorList() {

        for (int x = 0; x < raycastList.Count; x++) {
            Vector2 origin = new Vector2(raycastList[x].transform.position.x, raycastList[x].transform.position.y);

            Debug.DrawRay(origin, transform.TransformDirection(Vector2.down) * 1f, Color.red);
            Debug.DrawRay(origin, transform.TransformDirection(Vector2.up) * 1f, Color.red);
            Debug.DrawRay(origin, transform.TransformDirection(Vector2.left) * 1f, Color.red);
            Debug.DrawRay(origin, transform.TransformDirection(Vector2.right) * 1f, Color.red);
            
            for(int i = 0; i < 3; i++) {
                if(i==0) {
                    RaycastHit2D hit = Physics2D.Raycast(origin, transform.TransformDirection(Vector2.down), 1f);
                    if(hit) {
                        Conveyor conveyor = hit.collider.GetComponent<Conveyor>();
                        conveyorList.Add(conveyor);
                    }
                    
                }
                else if(i==1) {
                    RaycastHit2D hit = Physics2D.Raycast(origin, transform.TransformDirection(Vector2.up), 1f);
                    if(hit) {
                        Debug.Log("hit");
                        Conveyor conveyor = hit.collider.GetComponent<Conveyor>();
                        conveyorList.Add(conveyor);
                    }
                }
                else if(i==2) {
                    RaycastHit2D hit = Physics2D.Raycast(origin, transform.TransformDirection(Vector2.left), 1f);
                    if(hit) {
                        Conveyor conveyor = hit.collider.GetComponent<Conveyor>();
                        conveyorList.Add(conveyor);
                    }                
                }
                else {
                    RaycastHit2D hit = Physics2D.Raycast(origin, transform.TransformDirection(Vector2.right), 1f);
                    if(hit) {
                        Conveyor conveyor = hit.collider.GetComponent<Conveyor>();
                        conveyorList.Add(conveyor);
                    }                
                }
                
            }
            
            
        }
        return conveyorList;
    }

}
