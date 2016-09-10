using UnityEngine;
using System.Collections;

public class CrystalEnemyMovement : MonoBehaviour {


    void Awake() {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, (Vector2)transform.position + Random.insideUnitCircle * int.MaxValue, 1 << LayerMask.NameToLayer("Ground"), -20, 20);
        transform.position = hit.point;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
    }

}
