using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Testing : MonoBehaviour
{
    public Pathfinding pathfinding;

    public GameObject nonWalkableVisual;

    private GameObject wall;

    private PathfindingManager pathfindingManager;

    private void Awake() {
        pathfindingManager = GameObject.Find("Pathfinding Manager").GetComponent<PathfindingManager>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(2)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfindingManager.pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfindingManager.pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 1f + new Vector3(-99.5f, -99.5f, 0), new Vector3(path[i+1].x, path[i+1].y) * 1f + new Vector3(-99.5f, -99.5f, 0), Color.green, 5f);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfindingManager.pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfindingManager.pathfinding.GetNode(x,y).SetIsWalkable(false);
            pathfindingManager.pathfinding.GetNode(x,y).SetIsBreakable(true);
            Vector3 gridPos = pathfindingManager.pathfinding.GetGrid().GetWorldPosition(x,y); gridPos.x = gridPos.x + 0.5f; gridPos.y = gridPos.y + 0.5f;

            wall = Instantiate(nonWalkableVisual, gridPos, Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfindingManager.pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfindingManager.pathfinding.GetNode(x,y).SetIsWalkable(true);
            pathfindingManager.pathfinding.GetNode(x,y).SetIsBreakable(false);

            Destroy(wall);
        }
    }


}
