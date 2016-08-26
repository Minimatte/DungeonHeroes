using UnityEngine;
using System.Collections;
using System.Linq;

public class Shadowstep : DefensiveSpell {

    PlayerMovement2D movement;
    float distance = 4;
    LayerMask hitmask;
    float invulnerableTime = 0.5f;

    GameObject spellEffect;

    protected override void ActivateSpell() {
        Collider2D[] hits = Physics2D.OverlapCircleAll((Vector2)transform.position + Vector2.right * movement.GetRightValue * distance, distance, hitmask);
        if (hits.Length > 0) {
            var targets = hits.OrderBy(obj => Mathf.Abs(((Mathf.Atan2((obj.gameObject.transform.position - transform.position).y, (obj.gameObject.transform.position - transform.position).x) * Mathf.Rad2Deg))));


            GameObject go = (GameObject)Instantiate(spellEffect, transform.position, Quaternion.identity);
            go.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Destroy(go, 1f);

            GameObject target = targets.ToArray()[0].gameObject;
            transform.position = target.transform.position + target.transform.right * target.GetComponent<Enemy>().GetRightValue;

            if (Vector2.Dot(movement.GetRightVector, target.GetComponent<Enemy>().GetRightVector) > 0)
                movement.Flip();

            GetComponent<Health>().Invulnerable(invulnerableTime);
            Destroy(Instantiate(spellEffect, transform.position, Quaternion.identity), 1f);

        }
    }

    protected override void Init() {
        hitmask = (1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("FlyingEnemy"));
        movement = GetComponent<PlayerMovement2D>();
        spellName = "Shadowstep";
        manaCost = 1;
        spellEffect = Resources.Load<GameObject>("Shadowstep");
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + Vector2.right * movement.GetRightValue * distance, distance);
    }
}
