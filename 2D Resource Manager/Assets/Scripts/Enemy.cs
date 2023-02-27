using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    //Variable for enemy health
    public float health = 100f;
    
    private void Update() {
        //chacks if its health is less than or equal to zero if it is destroys the enemy
        if (health  <= 0f) {
            Destroy(gameObject);
        }
    }
}
