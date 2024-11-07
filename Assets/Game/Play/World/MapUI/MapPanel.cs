using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour
{
    public static Action onNewBattle;

    [SerializeField] private Image[] enemyImages;
    [SerializeField] private TextMeshProUGUI[] enemyCount;
    [Space]
    [SerializeField] private BattleMapManager battleMapManager;
    [HideInInspector] public Map chosenMap;

    public void StartMap()
    {
        if (chosenMap.mapState == Map.MapState.activate)
        {
            ClosePanel();
            
            battleMapManager.ActivateMap(chosenMap);
            onNewBattle.Invoke();
            
            SaveSystem.onSave.Invoke();
        }
    }

    public void ShowMap(Map map)
    {
        chosenMap = map;

        for (byte i = 0; i < enemyImages.Length; i++)
        {
            if (i < map.mapData.enemyPrefab.Length)
            {
                enemyImages[i].sprite = map.mapData.enemyPrefab[i].GetComponent<EnLogic>().image;
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
}
