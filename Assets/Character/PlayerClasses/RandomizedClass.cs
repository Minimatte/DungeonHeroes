using UnityEngine;
using System.Collections;
using System;


public class RandomizedClass : MonoBehaviour {

    public Stats playerStats;
    Sprite[] subsprites;
    public string spriteSheetName;

    private bool hasRandomSprite;
    private static RandomizedClass instance;
    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Setup() {
        Health hp = GetComponent<Health>();
        Mana mana = GetComponent<Mana>();
        
        hp.maxHealth = playerStats.baseHealth;
        mana.maxMana = playerStats.baseMana;
        hp.currentHealth = playerStats.baseHealth;
        mana.currentMana = playerStats.baseMana;
        if (!hasRandomSprite) {
            SetSprites();
            hasRandomSprite = true;
        }
    }


    private void SetSprites() {
        subsprites = Resources.LoadAll<Sprite>("Characters/" + spriteSheetName);
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>()) {
            string spritename = renderer.sprite.name;
            var newSprite = Array.Find(subsprites, item => item.name == spritename);

            if (newSprite)
                renderer.sprite = newSprite;
            else
                renderer.sprite = null;
        }
    }
}
