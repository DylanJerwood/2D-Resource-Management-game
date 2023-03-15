using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Enemy : MonoBehaviour
{   
    
    public float health = 100f;
    private const float speed = 3f;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    public bool setPath = true;
    
    private void Update() {
        //chacks if its health is less than or equal to zero if it is destroys the enemy
        if (health  <= 0f) {
            Destroy(gameObject);
        }

        HandleMovement();
        if(setPath == true) {
            SetTargetPosition(new Vector3(-51, -51, 0));
            setPath = false;
        }
        
        if (Input.GetMouseButtonDown(2)) {
            SetTargetPosition(UtilsClass.GetMouseWorldPosition());
        }
    }

    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.01f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            } 
            else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            }
        } 
    }

    private void StopMoving() {
        pathVectorList = null;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition) {
        currentPathIndex = 0;
        pathVectorList = null;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }
}
