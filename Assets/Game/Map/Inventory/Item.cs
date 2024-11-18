using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static Action onItemDrop;
    
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;

    public enum ItemType
    {
        character,
        ability,
        equip
    }

    [HideInInspector] public ItemType itemType; //айтем тайп теперь есть у скриптбл обджекта, то есть предмета напрямую, а этот скрипт надо подфиксить...

    [HideInInspector] public Transform originalParent;
    [HideInInspector] public ItemSO itemSO;

    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }


    public void RenderItem()
    {
        image.sprite = itemSO.image;
        itemName.text = itemSO.itemName;

        if (itemSO.itemPrefab.GetComponent<LogicCharacter>() == true)
        {
            itemType = ItemType.character;
        }
        else if (itemSO.itemPrefab.GetComponent<Ability>() == true)
        {
            itemType = ItemType.ability;
        }
        else if (itemSO.itemPrefab.GetComponent<Equipment>() == true)
        {
            itemType = ItemType.equip;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false; // отключаем блок рейкастов потому что он не пропускает проверки на OnDrop
        
        originalParent = transform.parent;
        transform.SetParent(transform.root);

        onItemDrop?.Invoke();
    }

    public void OnDrag(PointerEventData eventData) //https://www.youtube.com/watch?v=BGr-7GZJNXg&t=324s //можно переписать на рект трансформ, тем более что щас поломалось...
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        //eventData.pointerCurrentRaycast.screenPosition; сказал чел так надо двигать, а почему не знаю
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        rectTransform.DOAnchorPos(originalParent.position, 0.4f)
            .SetLink(gameObject);

        //transform.position = originalParent.position;
        transform.SetParent(originalParent);

        onItemDrop?.Invoke(); //подписаные слоты обновляют наличие предмета у персонажей
    }
}