using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryHandler : MonoBehaviour{

    public List<Item> inventory = new List<Item>();

    public int maxInventorySize = 10;

    public int inventorySize { get { return inventory.Count; } }

    public bool inventoryIsFull { get { return inventory.Count >= maxInventorySize; } }


    public KeyCode hotKeyForInventory = KeyCode.E;
    public GameObject InventoryCanvas;

    void Start() {
        
    }

    public bool addItem(Item item) {

        if (!inventoryIsFull) {
            inventory.Add(item);
            return true;
        }

        return false;
    }

    public bool removeItem(Item item) {

        if (inventory.Contains(item)) {
            inventory.Remove(item);

            GameObject.Find("GameEvents").GetComponent<GameEvents>().SpawnItem(item, (transform.position + (GetComponent<Transform>().right * 1f * transform.localScale.x    )));
            return true;
        }

        return false;
    }

    void Update() {
        if (Input.GetKeyDown(hotKeyForInventory)) {
            InventoryCanvas.SetActive(!InventoryCanvas.activeInHierarchy);
        }
    }

}
