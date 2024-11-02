using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    public GameObject itemPrefab;
    [Space]
    public Sprite image;
    public string itemName;
}
