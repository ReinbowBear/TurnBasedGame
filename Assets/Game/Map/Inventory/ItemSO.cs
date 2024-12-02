using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    public enum ItemType
    {
        enemy,
        
        character,
        ability,
        equip
    }

    public AssetReference itemPrefab;
    public ItemType itemType;
    [Space]
    public Sprite image;
    public string itemName;
    [Space]
    [TextArea(5, 0)]
    public string description;
}
