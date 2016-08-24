using UnityEngine;
using System.Collections;

public class Fireball : OffensiveSpell {

    public GameObject ProjectilePrefab;

    protected override void ActivateSpell() {

        GameObject go = ((GameObject)Instantiate(ProjectilePrefab, transform.position + Vector3.up * 0.16f, Quaternion.AngleAxis(90 - 90 * transform.localScale.x, Vector3.up)));
        go.layer = gameObject.layer;
        go.GetComponent<Projectile>().damage = power;
    }

    protected override void Init() {
        power = 10;
        manaCost = 5;
        spellName = "Fireball";
        ProjectilePrefab = Resources.Load<GameObject>("SpellPrefabs/SpellPrefab" + spellName);
    }

}
