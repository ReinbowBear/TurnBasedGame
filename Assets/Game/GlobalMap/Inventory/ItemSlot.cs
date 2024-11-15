using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Item item => GetComponentInChildren<Item>();

    public void AddItem(Item newItem)
    {
        newItem.transform.SetParent(transform, false); //false устанавливает объект относительно нового родителя, а не мировых координат
    }

    private void SwapItems(Item newItem)
    {
        item.transform.SetParent(newItem.originalParent, false);
        newItem.originalParent = transform; //дальше Item передвинет его собсвтенный OnEndDrag
    }


    public void OnDrop(PointerEventData eventData) //если Item реализует OnDrop то реагирует сам на себя после передвижения //это если он не отключает рейкаст тригер в канвас групе
    {
        if (item == null)
        {
            eventData.pointerDrag.GetComponent<Item>().originalParent = transform;
        }
        else
        {
            SwapItems(eventData.pointerDrag.GetComponent<Item>());
        }
    }
}
