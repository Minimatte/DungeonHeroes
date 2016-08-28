using UnityEngine;
using System.Collections;
    
public class ParallaxCamera : MonoBehaviour {
    public delegate void ParallaxCameraDelegate(Vector3 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPosition;
    private float oldPositiony;
    private float oldPositionz;

    void Start() {
        oldPosition = transform.position.x;
        oldPositiony = transform.position.y;
        oldPositionz = transform.position.z;
    }

    void Update() {
        if (transform.position.x != oldPosition || transform.position.y != oldPositiony || transform.position.z != oldPositionz) {
            if (onCameraTranslate != null) {
                Vector3 delta = new Vector3(oldPosition - transform.position.x, oldPositiony - transform.position.y, oldPositionz - transform.position.z);
                onCameraTranslate(delta);
            }
            oldPosition = transform.position.x;
            oldPositiony = transform.position.y;
            oldPositionz = transform.position.z;
        }
    }
}
