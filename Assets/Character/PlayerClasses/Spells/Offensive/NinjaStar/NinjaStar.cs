using UnityEngine;
using System.Collections;

public class NinjaStar : OffensiveSpell {

    public GameObject ProjectilePrefab;

    protected override void ActivateSpell() {

        GameObject star = Instantiate(ProjectilePrefab, transform.position, Quaternion.AngleAxis(90 - 90 * transform.localScale.x, Vector3.up)) as GameObject;

        star.GetComponent<NinjaStarProjectile>().damage = PlayerHeroes.GetPlayerPower + power;
    }

    protected override void Init() {
        power = 1;
        manaCost = 3;
        spellName = "NinjaStar";
        ProjectilePrefab = Resources.Load<GameObject>("SpellPrefabs/SpellPrefab" + spellName);
    }

}
