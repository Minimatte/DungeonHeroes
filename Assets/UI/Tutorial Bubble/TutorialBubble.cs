using UnityEngine;
using System.Collections;

public class TutorialBubble : MonoBehaviour {

    public GameObject controllerActive, controllerInActive;

    void Update() {
        bool active = Input.GetJoystickNames().Length > 0;
        controllerActive.SetActive(active);
        controllerInActive.SetActive(!active);


    }


}
