using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour {
    public List<DropData> dropList;
    private int itemsDropped;

    [Range(1, 10)]
    public int itemsDroppedMax;

    void Start() {
        itemsDropped = Random.Range(1, itemsDroppedMax);
    }

    public void DropItems() {
        for (int i = 0; i < itemsDropped; i++) {
            DropData data = dropList[Random.Range(0, dropList.Count)];
            
                Instantiate(data.prefab, transform.position, Quaternion.identity);
               

        }

        Destroy(gameObject);
    }
}

[System.Serializable]
public class DropData {
    public GameObject prefab;
    public int amount;
}
