using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    private PathfindingManager pathfindingManager;
    private GridBuildingSystem gridBuildingSystem;
    private Enemy enemy;
    public float health = 100;
    private Vector3 objectPosition;

    private void Awake() {
        pathfindingManager = GameObject.Find("Pathfinding Manager").GetComponent<PathfindingManager>();
        gridBuildingSystem = GameObject.Find("GridBuildingSystem").GetComponent<GridBuildingSystem>();
        objectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if(health <= 0) {
            gridBuildingSystem.ObjectDestroyed(objectPosition);
            pathfindingManager.setPath = true;
        }
    }
}
