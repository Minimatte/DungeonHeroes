using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Mana : MonoBehaviour {
    public float maxMana;
    public float currentMana;
    public float manaRegen = 1;

    public bool HasMana(float manaCost) {
        if (manaCost <= currentMana)
            return true;
        else
            return false;
    }

    public virtual void useMana(float manaToRemove) {
        currentMana -= manaToRemove;

    }

    public virtual void giveMana(float manaAmount) {
        currentMana += manaAmount;

        if (currentMana > maxMana)
            currentMana = maxMana;
    }

    void Update() {

        currentMana += manaRegen * Time.deltaTime;

        currentMana = Mathf.Clamp(currentMana, 0, maxMana);

    }

}
