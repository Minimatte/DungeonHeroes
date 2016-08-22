using UnityEngine;
using System.Collections;

public class Whirlwind : OffensiveSpell {

    private float cooldownRemain = 0;
    private float cooldown = 0.1f;
    private float aoe = 2;
    public LayerMask hitMask = ((1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("FlyingEnemy")));

    private bool active = false;
    private GameObject vfx;
    private GameObject vfxInstance;
    protected override void Init() {
        spellName = "Whirlwind";
        manaCost = 1;
        power = 5;
        vfx = Resources.Load<GameObject>("VFXWhirlwind");
    }

    private void DealDamageAOE() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, aoe, hitMask);

        for (int x = 0; x < hits.Length; x++) {
            if (hits[x].GetComponent<Health>()) {
                hits[x].GetComponent<Health>().takeDamage(power);
                print("hit");
            }
        }

    }

    protected override void ActivateSpell() {
        active = true;
        DealDamageAOE();
        base.ActivateSpell();

        if (vfx != null && vfxInstance == null) {
            vfxInstance = Instantiate(vfx, transform.position, Quaternion.identity) as GameObject;
            vfxInstance.transform.SetParent(transform, true);
        }

    }

    private void DeActivateSpell() {
        active = false;
        if (vfxInstance != null)
            Destroy(vfxInstance);
    }

    void Update() {
        if (Input.GetButtonUp("OffensiveSpell"))
            DeActivateSpell();
        if (active && cooldownRemain == 0) {
            cooldownRemain = cooldown;
            UseSpell();
        }
        if (cooldownRemain > 0)
            cooldownRemain = Mathf.Clamp(cooldownRemain - Time.deltaTime, 0, cooldown);

    }
}
