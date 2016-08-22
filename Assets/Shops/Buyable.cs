using UnityEngine;
using System.Collections;

public abstract class Buyable : MonoBehaviour {
    public int cost = 0;
    [Multiline]
    public string storeText;

    public abstract void Buy();
}
