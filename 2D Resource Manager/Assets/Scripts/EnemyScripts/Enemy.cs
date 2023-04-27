using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Enemy : MonoBehaviour
{   
    //Stats
    public float health = 100f;
    private const float speed = 3f;
    
    //variables for logic
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    public bool setPath = true;

    private void Update() {
        //chacks if its health is less than or equal to zero if it is destroys the enemy
        if (health  <= 0f) {
            Destroy(gameObject);
        }
        //moves the object to its target
        HandleMovement();
        //if setPath becomes true then it will recalculate a new path
        if(setPath == true) {
            if(GameObject.FindGameObjectsWithTag("Core").Length > 0) {
                GameObject closestCore = FindClosestObjectWithTag("Core");
                SetTargetPosition(closestCore.transform.position);
            }
            setPath = false;
        }
        //Debug tool to move the enemy objects as i see fit
        if (Input.GetMouseButtonDown(2)) {
            SetTargetPosition(UtilsClass.GetMouseWorldPosition());
        }
    }
    //function to move the object to a target
    private void HandleMovement() {
        //first checks to see if there is a path to follow
        if (pathVectorList != null) {
            //it then gets a target position from the list
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            //then while the object is not within a distance of 0.01
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
                //it gets a direction it needs to move in
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                //here is moves the transforms position in the direction at the speed set
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            //then when it is in a distance of 0.01 its reached its destination
            else {
                //so it moves to the next vector3 in the list
                currentPathIndex++;
                //but if the vector3 is the last in the list it calls StopMoving
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            }
        } 
    }
    //function to clear the vector3 list
    private void StopMoving() {
        pathVectorList = null;
    }
    //function to get the objects exact position
    public Vector3 GetPosition() {
        return transform.position;
    }  
    //function to set the objects target position
    public void SetTargetPosition(Vector3 targetPosition) {
        //resets the index
        currentPathIndex = 0;
        //clears the list
        pathVectorList = null;
        //repopulates the list with a new path
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        //if the list is valid
        if (pathVectorList != null && pathVectorList.Count > 1) {
            //removes the starting position from the list as it doesnt need to get there
            pathVectorList.RemoveAt(0);
        }
    }
    //function to get closest gameObject with a certain tag
    private GameObject FindClosestObjectWithTag(string tag) {
        GameObject[] listOfGameObjects = GameObject.FindGameObjectsWithTag(tag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestGameObject = null;

        foreach(GameObject Object in listOfGameObjects){
            float distanceToObject = Vector3.Distance(gameObject.transform.position, Object.transform.position);
            if (distanceToObject < shortestDistance) {
                shortestDistance = distanceToObject;
                nearestGameObject = Object;
            }
        }
        
        if(nearestGameObject != null) {
            return nearestGameObject;
        }

        return null;
    }
}
