using UnityEngine;
using System.Collections;

public struct ProjectileSettings {
    public float forwardSpeed;
    public bool useGravity;
    public bool destroyOnImpact;
    public float damage;
    public float lifetime;
    public bool useTrigger;
}

[System.Serializable]
public struct Stats {
    public float baseHealth;
    public float baseMana;
    public float expGrantedFromKill;
    public float movementSpeed;
    public float basePower;
    public int currentLevel;
}