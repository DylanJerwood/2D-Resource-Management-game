using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private Testing testscript;
    public float health = 100;

    private void Awake() {
        testscript = GameObject.Find("TestObject").GetComponent<Testing>();
    }

    private void Update()
    {
        if(health <= 0) {
            Vector3 objectWorldPosition = gameObject.transform.position;
            testscript.pathfinding.GetGrid().GetXY(objectWorldPosition, out int x, out int y);
            testscript.pathfinding.GetNode(x,y).SetIsWalkable(true);
            testscript.pathfinding.GetNode(x,y).SetIsBreakable(false);

            Destroy(gameObject); 
        }
    }
}
