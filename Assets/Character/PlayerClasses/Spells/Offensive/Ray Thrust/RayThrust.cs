using UnityEngine;
using System.Collections;

public class RayThrust : OffensiveSpell {

    GameObject ProjectilePrefab;
    float range = 3;
    PlayerMovement2D movement;

    protected override void ActivateSpell() {
        GameObject go = ((GameObject)Instantiate(ProjectilePrefab, transform.position + Vector3.up * 0.16f, Quaternion.AngleAxis(90 - 90 * transform.localScale.x, Vector3.up)));
        go.GetComponent<Projectile>().damage = power;
        go.GetComponent<RayThrustProjectile>().target = transform.position + (Vector3)Vector2.right * movement.GetRightValue * range;
        go.GetComponent<RayThrustProjectile>().owner = transform;
    }

    protected override void Init() {
        movement = GetComponent<PlayerMovement2D>();
        spellName = "RayThrust";
        manaCost = 10;
        power = 5;
        ProjectilePrefab = Resources.Load<GameObject>("SpellPrefabs/SpellPrefab" + spellName);
    }
}
