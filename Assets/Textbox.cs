using UnityEngine;
using System.Collections;

public class Textbox : MonoBehaviour {


    public Transform sign;

    // Update is called once per frame
    void Update() {
        transform.position = Camera.main.WorldToScreenPoint(sign.position + Vector3.up * 2f);
    }
}
