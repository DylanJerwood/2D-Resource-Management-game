using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int maxAmmo;
    public int ammoCount;

    public GameObject origin;
    public GameObject circle;

    public GameObject fireTarget;

    private void Update() {
        fireTarget = SearchForEnemy();

        if(ammoCount < 1) {

        }

    }

    private GameObject SearchForEnemy() {
    
        //Debug.Log("Gets Called");
        List<Collider2D> listOfEnemyColliders = new List<Collider2D>();
        GameObject enemy = null;
        //Variables for the OverlapCircle function
        float circleScale = 100f;
        ContactFilter2D contactFilter = new ContactFilter2D();

        int i = 0;

        Physics2D.OverlapCircle(origin.transform.position, circleScale, contactFilter, listOfEnemyColliders);
        
        foreach(Collider2D col in listOfEnemyColliders) {
           // Debug.Log("Foreach works");
            if(col) {
                
                enemy = col.GetComponent<Collider2D>().GetComponent<GameObject>();
                return enemy;
            }
            i++;
        }
        return enemy;
    }

    private void Fire() {

    }
}
