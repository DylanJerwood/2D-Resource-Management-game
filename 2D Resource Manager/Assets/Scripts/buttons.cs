using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    Vector2 mousePos;
    public GameObject drill;
    bool placeDrill = false;
    public GameObject mouseFollower;
    public GameObject hidden;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(placeDrill == true)
        {
            Vector3 tempx = new Vector3(Mathf.Round(mousePos.x * 1.0f) * 1f,0,0);
            Vector3 tempy = new Vector3(0,Mathf.Round(mousePos.y * 1.0f) * 1f,0);
            mouseFollower.transform.position = tempx + tempy;

            if (wrongplacement.can == true)
            {
                mouseFollower.GetComponent<Renderer>().material.color = new Color(0, 100, 0);
            }
            else
            {
                mouseFollower.GetComponent<Renderer>().material.color = new Color(100, 0, 0);
            }
        }
        else
        {
            mouseFollower.transform.position = hidden.transform.position;
        }

        if (Input.GetKeyDown("mouse 0") && placeDrill == true && wrongplacement.can == true)
        {
            Instantiate(drill, mouseFollower.transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown("mouse 1"))
        {
           placeDrill = false;
        }
    }

    

    public void drillButon()
    {
        placeDrill = true;
        mouseFollower = Instantiate(drill, mousePos, Quaternion.identity);
    }
}
