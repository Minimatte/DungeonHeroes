using UnityEngine;
using System.Collections;

public class RayThrustProjectile : Projectile {

    public Vector3 target;


    void Awake() {
 
    }

    void Update() {
        if (!canDamage)
            currentDamageCooldown = Mathf.Clamp(currentDamageCooldown - Time.deltaTime, 0, damageCooldown);
        var location = transform.position - target;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(location.x, location.y, location.z), 10f), 10 * Time.deltaTime);

    }


}
