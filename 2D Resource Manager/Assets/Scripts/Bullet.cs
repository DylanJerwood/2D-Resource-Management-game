using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variables needed for logic
    private Transform target;
    private float damage;
    private Enemy target_script;
    //variable to change speed at which bullet travels
    public float speed = 70f;

    //Function to get the variables we need from turret script
    public void Seek(Transform _target, float bulletDamage) {
        target = _target;
        damage =  bulletDamage;
        target_script = target.GetComponent<Enemy>();
    }

    void Update(){
        //if there isnt a target then immediatly destroy the bullet cause it shouldn't be there and dont to anything else
        if(target == null) {
            Destroy(gameObject);
            return;
        }
        //if there is a target then get the direction the bullet needs to go
        Vector3 dir = target.position -  transform.position;
        //Establish how fast the bullet will go per frame
        float distancePerFrame = speed * Time.deltaTime;
        //Check if the bullet has made it to its destination and if it has then call the hit target and dont do anything else
        if(dir.magnitude <= distancePerFrame) {
            HitTarget(damage);
            return;
        }
        //if it hasent move the bullet closer to the target
        transform.Translate(dir.normalized * distancePerFrame, Space.World);

    }
    //Function to device what will happen when the target is hit by a bullet
    void HitTarget(float bulletDamage) {
        //Lower the targets health by the bullet damage and then destroy the bullet
        target_script.health = target_script.health - bulletDamage;
        Destroy(gameObject);
    }
}
