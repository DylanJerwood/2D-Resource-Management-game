using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    public GameObject item;

    private void Awake() {
        item = gameObject;
    }
}
