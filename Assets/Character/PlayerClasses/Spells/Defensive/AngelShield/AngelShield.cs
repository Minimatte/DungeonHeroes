using UnityEngine;
using System.Collections;

public class AngelShield : DefensiveSpell {

    GameObject shieldPrefab;
    static GameObject shieldInstance;

    protected override void ActivateSpell() {
        shieldInstance = (GameObject)Instantiate(shieldPrefab, (Vector2)transform.position + Vector2.up * 0.16f, Quaternion.identity);
        shieldInstance.GetComponent<AngelShieldPrefab>().owner = transform;
    }

    protected override void Init() {
        spellName = "AngelShield";
        manaCost = 10;
        shieldPrefab = Resources.Load<GameObject>("AngelShield");
    }

    public override void UseSpell() {
        if (shieldInstance != null)
            return;

        base.UseSpell();
    }
}
