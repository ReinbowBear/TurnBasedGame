using System.IO;
using UnityEngine;

public class EntryPlay : MonoBehaviour
{
    [SerializeField] private GlobalMap globalMap;
    [SerializeField] private MapContent mapContent;
    [SerializeField] private GlobalMapManager globalMapManager;

    [SerializeField] private InventoryContent inventoryContent;

    void Awake()
    {
        CheckSave();
    }


    private void CheckSave()
    {
        if (File.Exists(SaveSystem.GetFileName()))
        {
            Debug.Log("LoadGame");
            SaveSystem.LoadGame();
        }
        else
        {
            globalMap.NewGlobalMap();
            mapContent.PrepareMaps();
            globalMapManager.StartLayer();

            inventoryContent.StartCharacters();
        }
    }
}
