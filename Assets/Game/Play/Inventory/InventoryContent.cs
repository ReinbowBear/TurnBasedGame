using UnityEngine;

public class InventoryContent : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [Space]
    [SerializeField] private ItemSO[] itemsSO;

    private SetInventory myInventory;

    void Awake()
    {
        myInventory = GetComponent<SetInventory>();
    }


    public void AddItem(ItemSO itemSO)
    {
        Item newItem = Instantiate(itemPrefab, transform).GetComponent<Item>();

        for (byte i = 0; i < myInventory.slots.Count; i++)
        {
            if (myInventory.slots[i].item == null)
            {
                myInventory.slots[i].AddItem(newItem);
                newItem.itemSO = itemSO;
                newItem.RenderItem();
                break;
            }
        }
    }


    private void Save()
    {
        SaveInventoryContent saveInventory = new SaveInventoryContent();

        saveInventory.itemsSO = new ItemSO[myInventory.slots.Count];

        for (byte i = 0; i < myInventory.slots.Count; i++)
        {
            if (myInventory.slots[i].item == null)
            {
                saveInventory.itemsSO[i] = myInventory.slots[i].item.itemSO;
            }
        }

        SaveSystem.gameData.saveInventoryContent = saveInventory; 
    }

    private void Load()
    {
        SaveInventoryContent saveInventory = SaveSystem.gameData.saveInventoryContent;

        for (byte i = 0; i < saveInventory.itemsSO.Length; i++)
        {
            AddItem(saveInventory.itemsSO[i]);
        }
    }


    void OnEnable()
    {
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
    }

    void OnDisable()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }
}


public struct SaveInventoryContent
{
    public ItemSO[] itemsSO;
}
