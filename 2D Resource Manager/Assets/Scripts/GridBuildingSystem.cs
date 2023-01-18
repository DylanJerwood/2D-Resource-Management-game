using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private PlacedObjectTypeSO placedObjectTypeSO;
    private Grid<GridObject> grid;

    private void Awake() {
        int gridWidth = 20;
        int gridHeight = 20;
        float cellSize = 3f;
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    public class GridObject {

        private Grid<GridObject> grid;
        private int x;
        private int y;
        private Transform transform;

        public GridObject(Grid<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this .y = y;
        }

        public void SetTransform(Transform transform) {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x,y);
        }

        public void ClearTransform(Transform transform) {
            transform = null;
            grid.TriggerGridObjectChanged(x,y);
        }

        public bool CanBuild() {
            return transform == null;
        }

        public override string ToString() {
            return x + "," + y + "/n" + transform;
        }
        
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);

            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x,y), PlacedObjectTypeSO.Dir.Down);

            GridObject gridObject = grid.GetGridObject(x,y);
            if (gridObject.CanBuild()) {
                Transform buildTransform = Instantiate(placedObjectTypeSO.prefab, grid.GetWorldPosition(x,y), Quaternion.identity);

                foreach(Vector2Int gridPosition in gridPositionList) {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(buildTransform);
                }
                gridObject.SetTransform(buildTransform); 
            }
            else {
                UtilsClass.CreateWorldTextPopup("Cannot build here!", UtilsClass.GetMouseWorldPosition());
            }
        }
    }
}
