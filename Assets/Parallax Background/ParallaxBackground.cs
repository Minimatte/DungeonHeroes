﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour {
    public ParallaxCamera parallaxCamera;

    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Awake() {
        if (!Application.isPlaying)
            return;

        if (parallaxCamera != null) {
            parallaxCamera.onCameraTranslate -= Move;
        }
        parallaxCamera = null;
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
        if (parallaxCamera != null) {
            parallaxCamera.onCameraTranslate += Move;
            if (GameEvents.player != null)
                transform.position = GameEvents.player.transform.position;
        }
        transform.position = new Vector3(parallaxCamera.transform.position.x, parallaxCamera.transform.position.y, transform.position.z);
        SetLayers();
    }

    void OnDestroy() {
        if (parallaxCamera != null) {
            parallaxCamera.onCameraTranslate -= Move;
            parallaxCamera = null;
        }
    }

    [ContextMenu("SetLayers")]
    public void SetLayers() {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++) {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null) {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }

    void Move(Vector3 delta) {
        foreach (ParallaxLayer layer in parallaxLayers) {
            layer.Move(delta);
        }
    }
}
