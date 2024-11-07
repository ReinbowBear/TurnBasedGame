using System.IO;
using UnityEngine;

public class EntryPoint : MonoBehaviour
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
        string mySave = SaveSystem.GetFileName();
        if (File.Exists(mySave))
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
