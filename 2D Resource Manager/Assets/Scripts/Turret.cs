using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Stats")]
    //Variables to change aspects of the turret
    public float range = 8f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountDown = 0f;
    public int maxAmmo;
    public int ammoCount;
    public float bulletDamage = 25f;

    [Header("Script Requirements")]
    //Variables to handle the logic
    public string enemyTag = "Enemy";
    private Transform target;
    public Transform origin;
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;

    
    private void Awake() {
        //Every .5 seconds it calles the UpdateTarget in order to look for a target 
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update() {
        //Making sure that if there is no target in its area then nothing will happen in order to make it more efficient
        if(target == null) {
            return;
        }
        //However if there is a target then i want the turret to follow it
        LockOnTarget();
        //If there is enough ammo and enough time has passed since last shot
        if(fireCountDown <= 0 && ammoCount > 0) {
            //Then take away from the ammo and shoot
            ammoCount = ammoCount - 1;
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }
    //Function in order to shoot a bullet
    void Shoot() {
        //Establish the gameobject for the bullet
        GameObject bulletFired = null;
        //If statement to check which barrel its going to shoot out from
        if(Vector3.Distance(firePoint1.position, target.transform.position) < Vector3.Distance(firePoint2.position, target.transform.position)) {
            //Instanciates the bullet at the firepoint
            bulletFired = (GameObject)Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        }
        else{
            bulletFired = (GameObject)Instantiate(bulletPrefab, firePoint2.position, firePoint1.rotation);
        }
        //Gets the script of the bullet
        Bullet bullet = bulletFired.GetComponent<Bullet>();
        //If it is a bullet
        if(bullet != null) {
            //Then calls on the seek funciton in the bullet script
            bullet.Seek(target, bulletDamage);
        }
    }
    //Function in order to find a target
    void UpdateTarget() {
        /*Establish variables needed such as:
        •List of GameObjects with the tag enemyTag
        •Float for the shortest distance between enemy and turret
        •Variable to store the GameObject for the enemy closest to the turret*/
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        //Loop to go through each enemy in the list
        foreach(GameObject enemy in enemies){
            //Gets the distance between the enemy and turret
            float distanceToEnemy = Vector3.Distance(origin.position, enemy.transform.position);
            //if its smallter that shortest distance
            if (distanceToEnemy < shortestDistance) {
                //Changes shortest distance and nearest enemy
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //if there is an enemy and shortestDistance is in range
        if(nearestEnemy != null && shortestDistance <= range) {
            //Change the target to that enemy
            target = nearestEnemy.transform;
        }
        else{   //If not...
            //Change it to nothing
            target = null;
        }
    }
    //Function in order to make the turret follow the target
    private void LockOnTarget() {
        //Gets difference between the targets position and the turrets
		Vector3 dir = target.position - origin.position;
        //Stores the direction it needs to look into a quanternion
		Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Stores how it needs to change its rotation to face the enemy
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //Uses that variable to change the rotation of the part that needs to rotate (only in the z angle)
		partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);
    }
    //Debugging device in order to show the range of the turret
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin.position, range);
    }
}
