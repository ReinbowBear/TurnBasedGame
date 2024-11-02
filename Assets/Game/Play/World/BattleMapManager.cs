using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> mapEnemys = new List<GameObject>();
    private GameObject myMapObject;
    private System.Random random;

    void Awake()
    {
        random = new System.Random(DateTime.Now.Millisecond);
    }


    public void ActivateMap(Map map)
    {        
        if (myMapObject != null)
        {
            Destroy(myMapObject);
        }

        myMapObject = Instantiate(map.mapData.mapPrefab, transform);

        RandomEnemyList(map);
    }

    private void RandomEnemyList(Map map)
    {
        for (byte i = 0; i < map.mapData.enemyPrefab.Length; i++)
        {
            for (byte ii = 0; ii < map.mapData.enemyCount[i]; ii++)
            {
                mapEnemys.Add(map.mapData.enemyPrefab[i]);
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
}
