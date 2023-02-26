using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float damage;
    private Enemy target_script;

    public float speed = 70f;

    public void Seek(Transform _target, float bulletDamage) {
        target = _target;
        damage =  bulletDamage;
        target_script = target.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update(){
        if(target == null) {
            Destroy(gameObject);
            return;
        }
        
        Vector3 dir = target.position -  transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distancePerFrame) {
            HitTarget(damage);
            return;
        }

        transform.Translate(dir.normalized * distancePerFrame, Space.World);

    }

    void HitTarget(float bulletDamage) {
        
        target_script.health = target_script.health - bulletDamage;
        Destroy(gameObject);
    }
}
