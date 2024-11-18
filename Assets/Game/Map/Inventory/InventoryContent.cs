using UnityEngine;

public class InventoryContent : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [Space]
    [SerializeField] private SetInventory myInventory;
    [Space]
    [SerializeField] private AbilitySlot[] characterSlots;

    public void StartCharacters()
    {
        for (byte i = 0; i < characterSlots.Length; i++)
        {
            int random = Random.Range(0, Content.data.characters.Length);
            
            addCharacter(Content.data.characters[random]);
        }
    }

    public void InventoryAddItem(ItemSO itemSO)
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

    public void addCharacter(ItemSO itemSO)
    {
        for (byte i = 0; i < characterSlots.Length; i++)
        {
            if (characterSlots[i].item == null)
            {
                Item newItem = Instantiate(itemPrefab, characterSlots[i].transform).GetComponent<Item>();
                newItem.itemSO = itemSO;
                newItem.RenderItem();
                break;
            }
        }
    }


    private void Save()
    {
        SaveInventoryContent saveInventory = new SaveInventoryContent();

        saveInventory.CharactersSO = new ItemSO[characterSlots.Length];
        saveInventory.itemsSO = new ItemSO[myInventory.slots.Count];

        for (byte i = 0; i < characterSlots.Length; i++)
        {
            if (characterSlots[i].item != null)
            {
                saveInventory.CharactersSO[i] = characterSlots[i].item.itemSO;
            }
        }

        for (byte i = 0; i < myInventory.slots.Count; i++)
        {
            if (myInventory.slots[i].item != null)
            {
                saveInventory.itemsSO[i] = myInventory.slots[i].item.itemSO;
            }
        }

        SaveSystem.gameData.saveInventoryContent = saveInventory; 
    }

    private void Load()
    {
        SaveInventoryContent saveInventory = SaveSystem.gameData.saveInventoryContent;

        for (byte i = 0; i < characterSlots.Length; i++)
        {
            addCharacter(saveInventory.CharactersSO[i]);
        }

        for (byte i = 0; i < saveInventory.itemsSO.Length; i++)
        {
            InventoryAddItem(saveInventory.itemsSO[i]);
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

[System.Serializable]
public struct SaveInventoryContent
{
    public ItemSO[] CharactersSO;
    public ItemSO[] itemsSO;
}
