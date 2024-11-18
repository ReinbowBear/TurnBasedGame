using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour
{
    public static Action onNewBattle;

    [SerializeField] private Image[] enemyImages;
    [SerializeField] private TextMeshProUGUI[] enemyCount;

    [HideInInspector] public Map chosenMap;

    public void StartMap()
    {
        if (chosenMap.mapState == Map.MapState.activate)
        {
            ClosePanel();

            onNewBattle.Invoke();
            SaveSystem.onSave.Invoke();
            Scene.Load(2);
        }
    }

    public void ShowMap(Map map)
    {
        chosenMap = map;

        for (byte i = 0; i < enemyImages.Length; i++)
        {
            if (i < map.mapData.enemyIndex.Length)
            {
                enemyImages[i].sprite = Content.data.enemys[map.mapData.enemyIndex[i]].image;
                enemyCount[i].text = map.mapData.enemyCount[i].ToString();
            }
            else
            {
                enemyImages[i].sprite = null;
                enemyCount[i].text = null;
            }
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }


    private void SaveMap()
    {
        SaveSystem.gameData.saveMapPanel.mapData = chosenMap.mapData;
    }


    void OnEnable()
    {
        SaveSystem.onSave += SaveMap;
    }

    void OnDisable()
    {
        SaveSystem.onSave -= SaveMap;
    }
}

[System.Serializable]
public struct SaveMapPanel
{
    public MapData mapData;
}
