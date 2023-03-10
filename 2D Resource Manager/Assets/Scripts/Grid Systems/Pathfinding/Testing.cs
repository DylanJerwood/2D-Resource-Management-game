using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Testing : MonoBehaviour
{
    private Pathfinding pathfinding;

    public GameObject nonWalkableVisual;

    private void Start() {
       pathfinding = new Pathfinding(50,50); 
    }

    private void Update() {
        if (Input.GetMouseButtonDown(2)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 1f + new Vector3(-99.5f, -99.5f, 0), new Vector3(path[i+1].x, path[i+1].y) * 1f + new Vector3(-99.5f, -99.5f, 0), Color.green, 5f);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x,y).SetIsWalkable(!pathfinding.GetNode(x,y).isWalkable);
            pathfinding.GetNode(x,y).SetIsBreakable(!pathfinding.GetNode(x,y).isBreakable);
            Vector3 gridPos = pathfinding.GetGrid().GetWorldPosition(x,y); gridPos.x = gridPos.x + 0.5f; gridPos.y = gridPos.y + 0.5f;

            Instantiate(nonWalkableVisual, gridPos, Quaternion.identity);
        }
    }


}
