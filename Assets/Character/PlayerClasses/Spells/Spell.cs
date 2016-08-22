using UnityEngine;
using System.Collections;
using System;

public class Spell : MonoBehaviour {

    public string spellName = "None";
    public bool passive = false;
    public float manaCost = 0;
    public Sprite image;

    void Awake() {
        Init();
        image = Resources.Load<Sprite>("SpellIcons/Icon" + spellName);
    }

    public virtual void UseSpell() {

        Mana mana = GetComponent<Mana>();

        if (mana.HasMana(manaCost)) {
            ActivateSpell();
            mana.useMana(manaCost);
        } else {
            UIManager.NotEnoughManaAnim();
        }
    }

    protected virtual void Init() { }

    protected virtual void ActivateSpell() { }

}
