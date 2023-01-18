using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] public List<PlacedObjectTypeSO> placedObjectTypeSOList;
    public PlacedObjectTypeSO placedObjectTypeSO;

    private Grid<GridObject> grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;
    private int gridWidth = 150;
    private int gridHeight = 150;
    private float cellSize = 1.5f;

    private void Awake() {

        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-100, -100, 0), (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));

        placedObjectTypeSO = null;
    }

    public class GridObject {

        private Grid<GridObject> grid;
        private int x;
        private int y;
        private PlacedObject placedObject;

        public GridObject(Grid<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this .y = y;
        }

        public void SetPlacedObject(PlacedObject placedObject) {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x,y);
        }

        public PlacedObject GetPlacedObject() {
            return placedObject;
        }

        public void ClearPlacedObject() {
            placedObject = null;
            grid.TriggerGridObjectChanged(x,y);
        }

        public bool CanBuild() {
            return placedObject == null;
        }

        public override string ToString() {
            return "";
        }
        
    }

    private void Update() {
        
        if(Input.GetMouseButtonDown(0)) {
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);

            if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight) {
                
                List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x,y), dir);

                bool canBuild = true;
                foreach (Vector2Int gridPosition in gridPositionList) {
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
                        canBuild = false;
                        break;
                    }
                }

                if (canBuild) {
                    Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                    Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) + new Vector3(rotationOffset.x, rotationOffset.y) * grid.GetCellSize();

                    PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x,y), dir, placedObjectTypeSO);
                    placedObject.transform.rotation = Quaternion.Euler(0, 0, -placedObjectTypeSO.GetRotationAngle(dir));
                    

                    foreach(Vector2Int gridPosition in gridPositionList) {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    }
                    
                }
                else {
                    UtilsClass.CreateWorldTextPopup("Cannot build here!", UtilsClass.GetMouseWorldPosition());
                }
            }
            else {
                UtilsClass.CreateWorldTextPopup("Cannot build here!", UtilsClass.GetMouseWorldPosition());
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            placedObjectTypeSO = null;
        }
    
         if (Input.GetKeyDown(KeyCode.F)) {
            GridObject gridObject = grid.GetGridObject(UtilsClass.GetMouseWorldPosition());
            PlacedObject placedObject = gridObject.GetPlacedObject();
            if(placedObject != null) {
                placedObject.DestroySelf();

                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                
                foreach(Vector2Int gridPosition in gridPositionList) {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            UtilsClass.CreateWorldTextPopup("" + dir, UtilsClass.GetMouseWorldPosition());

        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {placedObjectTypeSO = placedObjectTypeSOList[0]; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {placedObjectTypeSO = placedObjectTypeSOList[1]; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {placedObjectTypeSO = placedObjectTypeSOList[2]; }
        //if (Input.GetKeyDown(KeyCode.Alpha4)) {placedObjectTypeSO = placedObjectTypeSOList[3]; }
        //if (Input.GetKeyDown(KeyCode.Alpha5)) {placedObjectTypeSO = placedObjectTypeSOList[4]; }
    }
}
