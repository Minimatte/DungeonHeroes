using UnityEngine;
using System.Collections;

public class KnightShield : Health {

    void Update() {
        TakeDamage(Time.deltaTime);
    }

    public void OnDestroy() {
        Destroy(transform.parent.gameObject);
    }
}
