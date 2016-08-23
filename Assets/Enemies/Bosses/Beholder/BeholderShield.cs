using UnityEngine;
using System.Collections;

public class BeholderShield : Health {

    public override void Kill() {
        Destroy(gameObject);
    }

}
