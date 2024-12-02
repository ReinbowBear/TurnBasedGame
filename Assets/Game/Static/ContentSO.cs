using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "ContentSO", menuName = "ScriptableObject/Content")]
public class ContentSO : ScriptableObject
{
    public AssetReference[] maps;
    public ItemSO[] enemys;
    [Space]
    public ItemSO[] characters;
    public ItemSO[] items;
}
