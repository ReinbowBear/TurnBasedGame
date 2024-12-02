using UnityEngine;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour, IDropHandler
{    
    public Item item => GetComponentInChildren<Item>();

    [SerializeField] private ItemSO.ItemType myItemType;

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
            if (newItem.itemSO.itemType == myItemType)
            {
                newItem.originalParent = transform;
            }
        }
        else
        {
            Item newItem = eventData.pointerDrag.GetComponent<Item>();
            if (newItem.itemSO.itemType == myItemType)
            {
                SwapItems(newItem);
            }
        }
    }
}
