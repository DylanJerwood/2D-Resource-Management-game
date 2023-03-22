using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {
    //Variables for the grid and grid postition
    private Grid<PathNode> grid;
    public int x;
    public int y;
    //Variables for calculating the pathfinding
    public int gCost;
    public int hCost;
    public int fCost;
    //Variables for making the node walkable and breakable
    public bool isWalkable;
    public bool isBreakable;
    //variable to store the previous node in the path
    public PathNode cameFromNode;
    //Readies variables
    public PathNode(Grid<PathNode> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
        isBreakable = false;
    }
    //Function debug tool
    public override string ToString() {
        //return  x + "," + y;
        return "\n";
        //return fCost.ToString();
    }
    //Function for calculating the FCost
    public void CalculateFCost() {
        fCost = gCost+ hCost;
    }
    //Function for making the ground walkable or not walkable
    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x,y);
    }
    //Function for making the ground breakable or not breakable
    public void SetIsBreakable(bool isBreakable) {
        this.isBreakable = isBreakable;
        grid.TriggerGridObjectChanged(x,y);
    }
}

