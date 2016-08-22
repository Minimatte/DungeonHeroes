using UnityEngine;
[System.Serializable]
public class Item {

    public string itemName = "Item";
    public ItemType type = ItemType.ETC;
    public Sprite sprite;

    public override string ToString()
    {
        return itemName;
    }


}

public enum ItemType {
    ETC,
    Equip,
    Consumable
}
