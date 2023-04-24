using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {

    //Values for moving straight and diagonal (values multiplied by 10 in order to use Ints instead of floats)
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    //Instance of the script
    public static Pathfinding Instance { get; private set; }
    //lists for the grid and pathfinding lists
    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    //Function to create the gird itself
    public Pathfinding(int width, int height) {
        Instance = this;
        grid = new Grid<PathNode>(width, height, 1f, new Vector3(-100, -100, 0), (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y),false);
    }
    //Function to get the grid list
    public Grid<PathNode> GetGrid() {
        return grid;
    }
    //Function to get a list of Vector3 for an object to follow
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition) {
        //It gets the x and y postition for the start and end positions
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);
        //It then uses the FindPath function that takes ints in order to get a path it can then convert to Vector3
        List<PathNode> path = FindPath(startX, startY, endX, endY);

        //If a path can't be calculated
        if (path == null) {
            //Return nothing
            return null;
          //If one can...
        } else {
            //... create a list of Vector3
            List<Vector3> vectorPath = new List<Vector3>();
            //then for every node thats in the path previously calculated
            foreach (PathNode pathNode in path) {
                //its adds a Vector3 to the Vector3 path at the centre of the node
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() + new Vector3(-199, -199, 0) * grid.GetCellSize() * .5f);
            }
            //then once its finished it then returns the Vector3 path
            return vectorPath;
        }
    }
    //Function to create a path from a start position to an end position, it is done by checking if the path calculated is valid and changing the variables inside the pathnode
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY) {
        //it gets the actuall nodes for the start and end using the location given
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);
        //If either of them is null...
        if (startNode == null || endNode == null) {
            // ... then its an Invalid Path
            return null;
        }
        //Then create the lists for nodes that you need to search/that are on list list for the path and nodes that are already searched/will not be on the list for the path
        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();
        //For each pathnode in the grid it then readies then variables
        for (int x = 0; x < grid.GetWidth(); x++) {
            for (int y = 0; y < grid.GetHeight(); y++) {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }
        //Readies the gCost for the start node
        startNode.gCost = 0;
        //Calculates the hCost and fCost for the start node
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        //Then while the open list isn't empty
        while (openList.Count > 0) {
            //it gets the current node
            PathNode currentNode = GetLowestFCostNode(openList);
            //Checks if the current node is the end node
            if (currentNode == endNode) {
                // Reached final node and returns calculated path
                return CalculatePath(endNode);
            }
            //removes the current node from the open list and adds it to the closed
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            //then for every Pathnode around the current node
            foreach (PathNode neighbourNode in GetNeighbourList(currentNode)) {
                //checks if the node is already on the closed list (if it is it skips the rest)
                if (closedList.Contains(neighbourNode)) continue;
                //Checks that the node is not walkable and not breakable (any out of bounds areas)
                if(!neighbourNode.isWalkable && !neighbourNode.isBreakable) {
                    //If it is its added to the closed list and skips the rest rest
                    closedList.Add(neighbourNode); 
                    continue;
                }
                //Creates the tentative cost from the current node to the neighbour node
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                //if it is less than the gCost of the neighbour node then that is the node next in the path
                if (tentativeGCost < neighbourNode.gCost) {
                    //fills the neighbour nodes variables to make it then next one in the path
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();
                    //if its not already on the openlist then it is added
                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the openList/there is no available path to take
        return null;
    }
    //Function to get a list of PathNodes around a node
    private List<PathNode> GetNeighbourList(PathNode currentNode) {
        //list of neighbour nodes
        List<PathNode> neighbourList = new List<PathNode>();
        //Then goes through every node around it that it can walk to and checks if its viable if it is its added to the list
        if (currentNode.x - 1 >= 0) {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth()) {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        //once its finished it returns the list
        return neighbourList;
    }
    //Function to return a single node besed on its position
    public PathNode GetNode(int x, int y) {
        return grid.GetGridObject(x, y);
    }
    //Function to calculate a path
    private List<PathNode> CalculatePath(PathNode endNode) {
        //Creates a list for the path and adds the end node to it
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        //Creates a variable for the current node
        PathNode currentNode = endNode;
        //while the curent node has a node it came from/its not the start node
        while (currentNode.cameFromNode != null) {
            //it adds the current nodes previous node to the path
            path.Add(currentNode.cameFromNode);
            //and changes the current node to the previous node
            currentNode = currentNode.cameFromNode;
        }

        //When the path is calculated it then reverses it(as it was created back to front)
        //it also sets a bool called wal not found
        bool wallNodeFound = false; path.Reverse();
        //for every pathnode in the path
        for (int i = 0; i < path.Count; i++) {
            //it checks if the node is breakable(if it is it has a building constructed on it)
            if(path[i].isBreakable) {
                if(path[i] == endNode) {
                    endNode = path[i];
                    path.Remove(path[i]);
                    wallNodeFound = true;
                    break;
                }
                else{
                    //if it is it then removes the previous endNode
                    path.Remove(endNode);
                    //And changes the endNode to the new pathnode that has the building
                    endNode = path[i];
                    wallNodeFound = true;
                    break;
                    //it does this in order to check if a building is in the path
                }
            }
        }
        //if a building/wall was found in the path
        if(wallNodeFound == true) {
            //it then reconstructs the path with the new endnode being the node with the building
            path = new List<PathNode>();
            path.Add(endNode);
            currentNode = endNode;
            while (currentNode.cameFromNode != null) {
                path.Add(currentNode.cameFromNode);
                currentNode = currentNode.cameFromNode;
            }
            //once its finished it removes the end node
            path.Remove(endNode);
            //and reverses the path again
            path.Reverse();
        }
        //once its finished it then returns the path
        return path;
    }
    //Function to calculate the cost from moving from the start node to the end node
    private int CalculateDistanceCost(PathNode a, PathNode b) {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }
    //Function to sort through all the FCosts in a list an finding the smallest
    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList) {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++) {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost) {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

}
