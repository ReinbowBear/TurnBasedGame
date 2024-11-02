using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
public class CharacterData : ScriptableObject
{
    public byte damage;
    public byte maxDist;
    [Space]
    public byte health;
    public byte manna;
    public byte moveDistanse;
    [Space]
    [TextArea(5, 0)]
    [SerializeField] private string myComments;
}
