using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridBuildingSystem : MonoBehaviour
{
    //List for buildings
    public List<PlacedObjectTypeSO> placedObjectTypeSOList;
    //Variable for selected building
    public PlacedObjectTypeSO placedObjectTypeSO;

    //variables to set up grid
    private Grid<GridObject> grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;
    private int gridWidth = 200;
    private int gridHeight = 200;
    private float cellSize = 1f;

    //variable to check if the player is currently placing a building
    public bool placingObject;

    //variable for changing the pathfinding of enemies
    private PathfindingManager pathfindingManager;

    private void Awake() {

        //On awake this creates the gird using the public class Grid with Generic
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-100, -100, 0), (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y), false);

        //readies variables
        placedObjectTypeSO = null;
        placingObject = false;

        //Finds the script for the enemies pathfinding
        pathfindingManager = GameObject.Find("Pathfinding Manager").GetComponent<PathfindingManager>();
    }

    
    public class GridObject {

        private Grid<GridObject> grid;
        private int x;
        private int y;
        private PlacedObject placedObject;

        //gets x and y positions on the grid
        public GridObject(Grid<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        //Changes grid value to the placed object
        public void SetPlacedObject(PlacedObject placedObject) {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x,y);
        }

        //function to check the placed object in the grid space
        public PlacedObject GetPlacedObject() {
            return placedObject;
        }

        //function to clear grid space
        public void ClearPlacedObject() {
            placedObject = null;
            grid.TriggerGridObjectChanged(x,y);
        }

        //Checks to see if you can build there
        public bool CanBuild() {
            return placedObject == null;
        }

        //Writes string in the grid spaces
        public override string ToString() {
            //return null;
            //return x + ", " + y + "\n" + placedObject;
            return "\n" + placedObject;
            //return x + ", " + y;
        }
        
    }

    private void Update() {

        //Checks if the player selected a building
        if(placedObjectTypeSO != null) {

            //Checks if player pressed left click
            if(Input.GetMouseButtonDown(0)) {
                //Converts mouse position to an X and Y variable
                grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);

                //checks the player has pressed in the bounds of the grid
                if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight) {
                    
                    //Grabs the position for every cell the building will take up
                    List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x,y), dir);

                    //checks if the player can build there
                    bool canBuild = true;
                    //For every position the object will make it so you cant build there anymore
                    foreach (Vector2Int gridPosition in gridPositionList) {
                        if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
                            canBuild = false;
                            break;
                        }
                    }

                    //checks if the player can build
                    if (canBuild) {
                        //Establishes the rotation ofset you would need to place the object correctly
                        Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                        //places the object in each spot it will take up
                        Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) + new Vector3(rotationOffset.x, rotationOffset.y) * grid.GetCellSize();

                        //creates the object in the grid
                        PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x,y), dir, placedObjectTypeSO);
                        //chages its rotation
                        placedObject.transform.rotation = Quaternion.Euler(0, 0, -placedObjectTypeSO.GetRotationAngle(dir));
                        
                        //Changes the value of the grid to placed object
                        foreach(Vector2Int gridPosition in gridPositionList) {
                            grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                            pathfindingManager.pathfinding.GetNode(gridPosition.x,gridPosition.y).SetIsWalkable(false);
                            pathfindingManager.pathfinding.GetNode(gridPosition.x,gridPosition.y).SetIsBreakable(true);
                        }
                        pathfindingManager.setPath = true;
                    }
                    //If player cant build creates text to tell them 
                    else {
                        UtilsClass.CreateWorldTextPopup("Cannot build here!", UtilsClass.GetMouseWorldPosition());
                    }
                }
                else {
                    UtilsClass.CreateWorldTextPopup("Cannot build here!", UtilsClass.GetMouseWorldPosition());
                }
            }

            //checks if player pressed right click
            if (Input.GetMouseButtonDown(1)) {
                //removes selected building and changes it to nothing
                placedObjectTypeSO = null;
                placingObject = false;
            }
        
            //check if the player pressed R
            if (Input.GetKeyDown(KeyCode.R)) {
                //Changes the objects direction
                dir = PlacedObjectTypeSO.GetNextDir(dir);
                //Creates text telling player where it rotated
                UtilsClass.CreateWorldTextPopup("" + dir, UtilsClass.GetMouseWorldPosition());

            }
        }

        //checks player pressed F
        if (Input.GetKeyDown(KeyCode.F)) {
            //Destroys the object in that grid spot
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

        //Player can slect which building to use by pressing 1-4
        if (Input.GetKeyDown(KeyCode.Alpha1)) {placedObjectTypeSO = placedObjectTypeSOList[0]; placingObject = true; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {placedObjectTypeSO = placedObjectTypeSOList[1]; placingObject = true; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {placedObjectTypeSO = placedObjectTypeSOList[2]; placingObject = true; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {placedObjectTypeSO = placedObjectTypeSOList[3]; placingObject = true; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {placedObjectTypeSO = placedObjectTypeSOList[4]; placingObject = true; }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {placedObjectTypeSO = placedObjectTypeSOList[5]; placingObject = true; }

    }

    //Functions

    //Gets the snapped position for the mouse
    public Vector3 GetMouseWorldSnappedPosition() {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        grid.GetXY(mousePosition, out int x, out int y);

        if (placedObjectTypeSO != null) {
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) + new Vector3(rotationOffset.x, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        } else {
            return mousePosition;
        }
    }

    //Gets the placed objects rotation
    public Quaternion GetPlacedObjectRotation() {
        if (placedObjectTypeSO != null) {
            return Quaternion.Euler(0, 0, -placedObjectTypeSO.GetRotationAngle(dir));
        } else {
            return Quaternion.identity;
        }
    }

    public void ObjectDestroyed(Vector3 objectPosition) {
        GridObject gridObject = grid.GetGridObject(objectPosition);
        PlacedObject placedObject = gridObject.GetPlacedObject();
        
        placedObject.DestroySelf();

        List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();

        foreach(Vector2Int gridPosition in gridPositionList) {
            grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
            pathfindingManager.pathfinding.GetNode(gridPosition.x,gridPosition.y).SetIsWalkable(true);
            pathfindingManager.pathfinding.GetNode(gridPosition.x,gridPosition.y).SetIsBreakable(false);
        }
    }

    public void PlaceObjectOnAwake(PlacedObjectTypeSO objectToPlace, Vector3 positionToPlace) {
        grid.GetXY(positionToPlace, out int x, out int y);
        PlacedObject placedObject = PlacedObject.Create(positionToPlace, new Vector2Int(x,y), dir, objectToPlace);
        List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
        
        foreach(Vector2Int gridPosition in gridPositionList) {
            grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            pathfindingManager.pathfinding.GetNode(gridPosition.x,gridPosition.y).SetIsWalkable(false);
            pathfindingManager.pathfinding.GetNode(gridPosition.x,gridPosition.y).SetIsBreakable(true);
        }
    }
}
