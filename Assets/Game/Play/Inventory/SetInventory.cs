using System.Collections.Generic;
using UnityEngine;

public class SetInventory : MonoBehaviour
{
    [SerializeField] private GameObject ItemSlotPrefab;
    [SerializeField] private byte slotsCount;

    [HideInInspector] public List<ItemSlot> slots = new List<ItemSlot>();

    void Awake()
    {
        CreateSlots(slotsCount);
    }

    private void CreateSlots(byte slotsCount) //сомневаюсь что будет что либо на расширение инвентаря или прочие манипуляции, но как идея...
    {
        for (byte i = 0; i < slotsCount; i++)
        {
            ItemSlot SlotScript = Instantiate(ItemSlotPrefab, transform).GetComponent<ItemSlot>();
            slots.Add(SlotScript);
        }
    }
}
