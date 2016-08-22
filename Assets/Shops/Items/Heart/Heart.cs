using UnityEngine;
using System.Collections;
using System;

public class Heart : Buyable {
    public float HealthAmount = 10;

    public void Awake() {
        SetCost();
    }

    private void SetCost() {
        if (PlayerItems.gold <= 20)
            cost = 20 + (int)(PlayerItems.gold * UnityEngine.Random.Range(0.05f, 0.25f));
        else
            cost = (int)(20 + PlayerItems.gold * UnityEngine.Random.Range(0.05f, 0.25f));
    }

    public override void Buy() {
        GameEvents.player.GetComponent<Health>().heal(HealthAmount);
        Destroy(gameObject);
    }
}
