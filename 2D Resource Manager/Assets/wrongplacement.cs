using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrongplacement : MonoBehaviour
{
    public static bool can = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(can);
    }

    void OnTriggerEnter()
    {
        can = false;
        
    }

    void OnTriggerExit()
    {
        can = true;
    }
}
