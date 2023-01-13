using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    Vector2 mousePos;
    public GameObject drill;
    bool placeDrill = false;
    GameObject mouseFollower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePos);

        if(placeDrill == true)
        {
            mouseFollower.transform.position = mousePos;
        }

        if (Input.GetKeyDown("mouse 0") && placeDrill == true)
        {
            Instantiate(drill, mousePos, Quaternion.identity);
        }

        if (Input.GetKeyDown("mouse 1"))
        {
           placeDrill = false;
        }
    }

    public void drillButon()
    {
        Debug.Log("pressed");
        placeDrill = true;
        mouseFollower = Instantiate(drill, mousePos, Quaternion.identity);
    }
}
