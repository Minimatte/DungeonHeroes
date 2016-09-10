using UnityEngine;
using System.Collections;
using System.Linq;

public class CrystalAI : Enemy {

    public GameObject projectile;
    public Transform projectileSpawnPos;
    Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    protected override void Attack() {
        anim.SetTrigger("Attack");
        attackProperties.cooldown = 1;
    }


    public void SpawnProjectile() {
        
        Instantiate(projectile, projectileSpawnPos.position, Quaternion.FromToRotation(Vector2.right, ((Vector2)target.transform.position + Vector2.up * 0.16f) - (Vector2)transform.position));
    }

    void Update() {
        if (target != null) {
            if (attackProperties.cooldown == 0) {

                Attack();
            }

            if (Vector2.Distance(transform.position, target.transform.position) > aiProperties.aggroRange)
                target = null;
        } else
            CheckForTarget();

        attackProperties.cooldown = Mathf.Clamp(attackProperties.cooldown - Time.deltaTime, 0, 10);
    }
}
