using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    //Variables
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Camera Camera;


    void FixedUpdate()
    {
        //Gather Inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        //Execute player movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        HandleZoom();
    }



    private void HandleZoom()
    {
        //sets the minimum and maximum a player can zoom in or out
        float minZoom = 1f;
        float maxZoom = 30f;

        //If the value of your scroll wheel input is above 0 you zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //checks you arent at the mimimum zoom
            if (Camera.orthographicSize > minZoom)
            {
                Camera.orthographicSize--;
            }
        }

        //If the value of your scroll wheel input is below 0 you zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //checks you arent at the maximum zoom
            if (Camera.orthographicSize < maxZoom)
            {
                Camera.orthographicSize++;
            }
        }
    }
}
