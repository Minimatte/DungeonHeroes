using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class WindowPopup : MonoBehaviour {

    protected GameObject textbox;
    protected GameObject playerUI;


    void Awake() {
        playerUI = GameObject.Find("PlayerUI");
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        OnEnter(collision);
    }

    public void OnTriggerExit2D(Collider2D collision) {
        OnExit(collision);
    }

    protected virtual void OnEnter(Collider2D collision) {
        if (collision.CompareTag("Player") && textbox == null) {
            textbox = Instantiate(Resources.Load<GameObject>("Textbox")) as GameObject;
            textbox.transform.SetParent(playerUI.transform, false);
            textbox.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2.5f);
            textbox.GetComponent<RectTransform>().localScale = Vector3.one;
            //textbox.GetComponentInChildren<Text>().text = signText;
            textbox.GetComponent<Textbox>().sign = transform;
        }
    }

    protected virtual void OnExit(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Destroy(textbox);
            textbox = null;
        }
    }
}
