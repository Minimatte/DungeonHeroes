using UnityEngine;
using System.Collections;

public class Fireball : OffensiveSpell {

    public GameObject ProjectilePrefab;

    protected override void ActivateSpell() {

        ((GameObject)Instantiate(ProjectilePrefab, transform.position, Quaternion.AngleAxis(90 - 90 * transform.localScale.x, Vector3.up))).layer = gameObject.layer;

    }

    protected override void Init() {
        manaCost = 10;
        spellName = "Fireball";
        ProjectilePrefab = Resources.Load<GameObject>("SpellPrefabs/SpellPrefab" + spellName);
    }

}
