using UnityEngine;
using System.Collections;

public class UIControllerCheck : MonoBehaviour {

    public GameObject controllerImage;
    public GameObject keyboardHotkey;

    void Awake() {
        if (CheckForController())
            EnableController();
        else
            DisableController();
    }

    void Update() {
        if (CheckForController())
            EnableController();
        else
            DisableController();
    }

    void EnableController() {
        controllerImage.SetActive(true);
        keyboardHotkey.SetActive(false);
    }

    void DisableController() {
        controllerImage.SetActive(false);
        keyboardHotkey.SetActive(true);
    }

    bool CheckForController() {

        return Input.GetJoystickNames().Length > 0;
    }
}
