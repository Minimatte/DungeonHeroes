using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sign : WindowPopup {

    [Multiline]
    public string signText;

    protected override void OnEnter(Collider2D collision) {
        base.OnEnter(collision);
        if (collision.CompareTag("Player") && textbox != null) {
            textbox.GetComponentInChildren<Text>().text = signText;
        }
    }

    protected override void OnExit(Collider2D collision) {
        base.OnExit(collision);
        if (collision.CompareTag("Player")) {
            Destroy(textbox);
            textbox = null;
        }
    }
}
