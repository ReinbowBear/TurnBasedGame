using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    public enum ItemType
    {
        character,
        ability,
        equip
    }

    public GameObject itemPrefab;
    public ItemType itemType;
    [Space]
    public Sprite image;
    public string itemName;
}
