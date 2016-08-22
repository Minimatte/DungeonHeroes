using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventoryItemButton : MonoBehaviour {

    void Awake() {

        EventTrigger trigger = GetComponent<EventTrigger>(); 

        
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { GetComponentInParent<InventoryUI>().UpdateInventory(); });
        trigger.triggers.Add(entry);
    }
	
}
