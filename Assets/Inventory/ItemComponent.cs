using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ItemComponent : MonoBehaviour {

    public Item item;

    void Start() {
        if (GetComponentInChildren<SpriteRenderer>()) {
            item.sprite = GetComponentInChildren<SpriteRenderer>().sprite;
            setItemSprite(item.sprite);
        }
        gameObject.name = item.itemName;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {


            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());


            if (other.gameObject.GetComponent<InventoryHandler>().addItem(item)) {
                if (GameObject.Find("InventoryPanel")) {
                    GameObject.Find("InventoryPanel").GetComponentInChildren<InventoryUI>().UpdateInventory();
                }
                Destroy(gameObject);
            }
        }
    }

    public void setItemSprite(Sprite s) {
        GetComponentInChildren<SpriteRenderer>().sprite = s;
    }

    public void throwAway() {
        GameEvents.player.GetComponent<InventoryHandler>().removeItem(item);

    }

#if UNITY_EDITOR
    void Update() {
        if (GetComponentInChildren<SpriteRenderer>())
            setItemSprite(item.sprite);

        gameObject.name = item.itemName;

    }

#endif
}
