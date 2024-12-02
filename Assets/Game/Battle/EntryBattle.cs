using System;
using System.Collections.Generic;
using UnityEngine;

public class EntryBattle : MonoBehaviour
{
    [SerializeField] private GetCharacter getCharacter;
    [SerializeField] private StartCam startCam;

    [HideInInspector] public List<GameObject> mapEnemys = new List<GameObject>();

    private System.Random random;

    void Awake()
    {
        random = new System.Random(DateTime.Now.Millisecond);

        ActivateMap(SaveSystem.gameData.saveMapPanel.mapData);
    }


    public async void ActivateMap(MapData mapData)
    {        
        await Content.GetAsset(Content.data.maps[mapData.mapIndex], transform);

        getCharacter.NewDrop();
        startCam.SetCam();

        RandomEnemyList(mapData);
    }


    private void RandomEnemyList(MapData mapData)
    {
        for (byte i = 0; i < mapData.enemyIndex.Length; i++)
        {
            for (byte ii = 0; ii < mapData.enemyCount[i]; ii++)
            {
                Debug.Log("ещё не готово");
                //mapEnemys.Add(Content.data.enemys[mapData.enemyIndex[i]].itemPrefab);
            }
        }
        Shuffle(mapEnemys);
    }

    void Shuffle<T>(List<T> list) //перемешиваем лист хитрым алгоритмом с инетика
    {
        int count = list.Count;
        while (count > 1)
        {
            int randomIndex = random.Next(0, count--);
            T temp = list[count];
            list[count] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }


    public void EndBattle()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GetCharacter.characterList.Clear();
    }


    void OnEnable()
    {
        TurnManager.onEndLevel += EndBattle;
    }

    void OnDisable()
    {
        TurnManager.onEndLevel -= EndBattle;
    }    
}
