using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelDensity : MonoBehaviour {

    public float pixelsPerUnit = 100;

    void Update() {
        if (GetComponent<Camera>())
            GetComponent<Camera>().orthographicSize = Screen.height / pixelsPerUnit / 2;
        else
            transform.localScale = new Vector3(Screen.height / pixelsPerUnit / 2, transform.localScale.x, transform.localScale.z);
    }
}
