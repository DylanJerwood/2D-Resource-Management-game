using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Stats")]

    public float range = 8f;
    public float fireRate = 1f;
    private float fireCountDown = 0f;
    public int maxAmmo;
    public int ammoCount;
    public float turnSpeed = 10f;

    [Header("Script Requirements")]

    public string enemyTag = "Enemy";
    public Transform partToRotate;
    private Transform target;
    public Transform origin;

    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;


    private void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update() {
        if(target == null) {
            return;
        }

        LockOnTarget();

        if(fireCountDown <= 0 && ammoCount > 0) {
            ammoCount = ammoCount - 1;
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    void Shoot() {
        GameObject bulletFired = null;
        if(Vector3.Distance(firePoint1.position, target.transform.position) < Vector3.Distance(firePoint2.position, target.transform.position)) {
            bulletFired = (GameObject)Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        }
        else{
            bulletFired = (GameObject)Instantiate(bulletPrefab, firePoint2.position, firePoint1.rotation);
        }
        Bullet bullet = bulletFired.GetComponent<Bullet>();

        if(bullet != null) {
            bullet.Seek(target);
        }
    }

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies){
            float distanceToEnemy = Vector3.Distance(origin.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        }
        else{
            target = null;
        }
    }

    private void LockOnTarget() {
		Vector3 dir = target.position - origin.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin.position, range);
    }
}
