using UnityEngine;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour, IDropHandler
{    
    public Item item => GetComponentInChildren<Item>();

    public enum ItemType
    {
        character,
        ability,
        equip
    }
    [SerializeField] private ItemType myItemType;

    private void SwapItems(Item newItem)
    {
        item.transform.SetParent(newItem.originalParent, false);
        newItem.originalParent = transform; //дальше Item передвинет его собсвтенный OnEndDrag
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (item == null)
        {
            Item newItem = eventData.pointerDrag.GetComponent<Item>();
            if ((byte)myItemType == ((byte)newItem.itemType))
            {
                newItem.originalParent = transform;
            }
        }
        else
        {
            Item newItem = eventData.pointerDrag.GetComponent<Item>();
            if ((byte)myItemType == ((byte)newItem.itemType))
            {
                SwapItems(newItem);
            }
        }
    }
}
