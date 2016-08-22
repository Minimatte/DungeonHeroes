using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[ExecuteInEditMode]
public class InventoryUI : MonoBehaviour {
    [Range(0.0f, 100f)]
    public float itemHeight = 30, itemWidth = 30;
    [Range(0.0f, 30.0f)]
    public float spacingX = 0, spacingY = 0;


    public Image UIImage;

    Image inventorySlot;

	void OnEnable () {

        UpdateInventory();
	}


    public void UpdateInventory() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        InventoryHandler handler = GameEvents.player.GetComponent<InventoryHandler>();

        List<Item> inventory = handler.inventory;

        GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
        grid.spacing = new Vector2(spacingX, spacingY);
        grid.cellSize = new Vector2(itemWidth, itemHeight);


        for (int i = 0; i < inventory.Count; i++) {
            inventorySlot = (Image)Instantiate(UIImage);
            inventorySlot.transform.SetParent(transform, false);
            inventorySlot.GetComponent<Image>().sprite = inventory[i].sprite;
            inventorySlot.GetComponent<ItemComponent>().item = inventory[i];

        }
    }
}
