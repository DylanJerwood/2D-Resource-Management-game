using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int maxAmmo;
    public int ammoCount;

    public Transform target;
    public Transform origin;
    public float range = 8f;

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    private void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update() {
        if(target == null) {
            return;
        }

        LockOnTarget();
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
