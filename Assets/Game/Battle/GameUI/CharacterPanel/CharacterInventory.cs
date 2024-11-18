using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private AbilitySlot[] mySlots;
    [HideInInspector] public Item[] characterItems;

    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private Button button;

    void Awake()
    {
        characterItems = new Item[mySlots.Length];
    }


    public void OpenInventory()
    {
        if (!inventoryPanel.gameObject.activeSelf)
        {
            inventoryPanel.gameObject.SetActive(true);

            inventoryPanel.DORotate(new Vector3(0, 0, 0), 0.4f)
            .SetLink(gameObject);
        }
        else
        {
            inventoryPanel.DORotate(new Vector3(0, -90, 0), 0.4f)
            .SetLink(gameObject)
            .OnComplete(() => { inventoryPanel.gameObject.SetActive(false); });
        }
    }

    private void ReadItems()
    {
        for (byte i = 0; i < mySlots.Length; i++)
        {
            characterItems[i] = mySlots[i].item;
        }
    }


    public void SetButton()
    {
        if (button.enabled == true)
        {
            button.enabled = false;
        }
        else
        {
            button.enabled = true;
        }
    }


    void OnEnable()
    {
        MapPanel.onNewBattle += SetButton;
        TurnManager.onEndLevel += SetButton;
    }

    void OnDisable()
    {
        MapPanel.onNewBattle -= SetButton;
        TurnManager.onEndLevel -= SetButton;
    }
}
