using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class testing : MonoBehaviour
{

    private Grid grid;

    private void Start() {
        Grid grid = new Grid(4, 2, 10f);
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
               
        }
    }
}
