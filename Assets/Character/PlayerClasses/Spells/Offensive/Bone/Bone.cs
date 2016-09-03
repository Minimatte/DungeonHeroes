using UnityEngine;
using System.Collections;

public class Bone : OffensiveSpell {
    public GameObject ProjectilePrefab;

    protected override void ActivateSpell() {

        GameObject go = ((GameObject)Instantiate(ProjectilePrefab, transform.position + Vector3.up * 0.16f, Quaternion.AngleAxis(90 - 90 * transform.localScale.x, Vector3.up)));
        go.GetComponent<Projectile>().damage = power + PlayerHeroes.GetPlayerPower;
    }

    protected override void Init() {
        power = 3;
        manaCost = 5;
        spellName = "Bone";
        ProjectilePrefab = Resources.Load<GameObject>("SpellPrefabs/SpellPrefab" + spellName);
    }
}
