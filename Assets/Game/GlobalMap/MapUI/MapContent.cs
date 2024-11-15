using System.Collections.Generic;
using UnityEngine;

public class MapContent : MonoBehaviour
{
    [SerializeField] private GlobalMap globalMap;
    [SerializeField] private MapPanel mapPanel;

    private List<GameObject> maps;
    [Space]
    [SerializeField] private byte minEnemyCount;
    [SerializeField] private byte maxEnemyCount;
    [SerializeField] private byte[] EnemyTypesCount;

    private System.Random random;

    public void PrepareMaps()
    {
        random = globalMap.random;
        maps = new List<GameObject>(Content.data.maps);

        for (byte i = 0; i < globalMap.mapsNumber.Length; i++)
        {
            for (byte ii = 0; ii < globalMap.pathCount; ii++)
            {
                for (byte iii = 0; iii < globalMap.mapHeight; iii++)
                {
                    Map map = globalMap.pathPoints[i, ii, iii];
                    map.index = new byte[] { i, ii, iii };
                    map.mapPanel = mapPanel;
                    map.mapData = GetMapData(iii);
                }
            }
        }
    }

    private MapData GetMapData(byte mapHeight)
    {
        MapData mapData = new MapData();

        mapData.mapIndex = random.Next(0, maps.Count); //нужен лист индексов которые будем удалять что бы карты не повторялись

        mapData.enemyIndex = new int[EnemyTypesCount[mapHeight]];
        for (byte i = 0; i < EnemyTypesCount[mapHeight]; i++)
        {
            mapData.enemyIndex[i] = random.Next(0, Content.data.enemys.Length);
        }

        mapData.enemyCount = new int[EnemyTypesCount[mapHeight]];
        for (byte i = 0; i < EnemyTypesCount[mapHeight]; i++)
        {
            mapData.enemyIndex[i] = random.Next(minEnemyCount, maxEnemyCount);
        }

        return mapData;
    }
}


public struct MapData
{
    public int mapIndex;
    //public GameObject batleTarget;

    public int[] enemyIndex;
    public int[] enemyCount;
}

