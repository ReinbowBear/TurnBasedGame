using UnityEngine;

public class Content : MonoBehaviour
{
    public static Content data;

    void Awake()
    {
        data = this;
    }


    public GameObject[] maps;
    public ItemSO[] enemys;
    [Space]
    public ItemSO[] characters;
    public ItemSO[] items;
}
