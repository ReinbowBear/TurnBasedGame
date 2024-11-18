using System.IO;
using UnityEngine;

public class EntryGlobalMap : MonoBehaviour
{
    [SerializeField] private GlobalMap globalMap;
    [SerializeField] private MapContent mapContent;
    [SerializeField] private GlobalMapManager globalMapManager;
    [Space]
    [SerializeField] private InventoryContent inventoryContent;

    void Start()
    {
        CheckSave();
    }


    private void CheckSave()
    {
        if (File.Exists(SaveSystem.GetFileName()))
        {
            Debug.Log("LoadGame");
            SaveSystem.onLoad.Invoke();
        }
        else
        {
            globalMap.NewGlobalMap();
            mapContent.PrepareMaps();
            globalMapManager.StartLayer();

            inventoryContent.StartCharacters();

            SaveSystem.onSave.Invoke();
            SaveSystem.SaveFile();
        }
    }
}
