using UnityEngine;
using System.Collections;

public class Shield : DefensiveSpell {

    GameObject shieldPrefab;
    PlayerMovement2D movement;


    protected override void ActivateSpell() {
        GameObject instance = (GameObject)Instantiate(shieldPrefab, transform.position + movement.GetRightVector + (Vector3)Vector2.up * 0.16f, Quaternion.identity);
        instance.transform.localScale = new Vector3(instance.transform.localScale.x * movement.GetRightValue, instance.transform.localScale.y, instance.transform.localScale.z);
    }

    protected override void Init() {
        spellName = "Shield";
        shieldPrefab = Resources.Load<GameObject>("KnightShieldPrefab");
        manaCost = 10;
        movement = GetComponent<PlayerMovement2D>();
    }
}
