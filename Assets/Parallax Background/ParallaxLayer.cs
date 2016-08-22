using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour {
    [Range(0, 1)]
    public float parallaxFactorX;
    [Range(0, 1)]
    public float parallaxFactorY;
    [Range(0, 1)]
    public float parallaxFactorZ;
    public bool ScaledToX = false;

    public void Move(Vector3 delta) {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta.x * parallaxFactorX;
        newPos.y -= delta.y * parallaxFactorY;
        newPos.z -= delta.z * parallaxFactorZ;
        transform.localPosition = newPos;
    }
#if UNITY_EDITOR
    void Update() {
        if (ScaledToX) {
            parallaxFactorY = parallaxFactorX;
            parallaxFactorZ = parallaxFactorX;
        }
    }
#endif
}
