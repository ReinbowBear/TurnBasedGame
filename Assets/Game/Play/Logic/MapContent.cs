using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapContent : MonoBehaviour
{
    [SerializeField] private GlobalMap globalMap;
    [SerializeField] private MapPanel mapPanel;
    [Space]
    [SerializeField] private List<GameObject> maps = new List<GameObject>();
    private List<GameObject> usedMaps = new List<GameObject>();
    [SerializeField] private GameObject[] enemy;
    [Space]
    [SerializeField] private byte minEnemyCount;
    [SerializeField] private byte maxEnemyCount;
    [SerializeField] private byte[] EnemyTypesCount;

    private System.Random random;

    void Start()
    {
        random = globalMap.random;
        PrepareMaps();
    }


    private void PrepareMaps()
    {
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
        GameObject randomMap = maps[random.Next(0, maps.Count)];
        usedMaps.Add(randomMap);
        //maps.Remove(randomMap); недостаточно уникальных карт на всю игру

        //HashSet<GameObject> randomEnemys = new HashSet<GameObject>();
        List<GameObject> randomEnemys = new List<GameObject>();
        while (randomEnemys.Count < EnemyTypesCount[mapHeight])
        {
            int randomEnemy = random.Next(0, enemy.Length);
            randomEnemys.Add(enemy[randomEnemy]);
        }

        List<int> enemyCounts = new List<int>();
        while (enemyCounts.Count < EnemyTypesCount[mapHeight])
        {
            int randomEnemyCount = random.Next(minEnemyCount, maxEnemyCount);
            enemyCounts.Add(randomEnemyCount);
        }

        MapData mapData = new MapData();
        mapData.SetMapData(randomMap, randomEnemys.ToArray(), enemyCounts.ToArray());
        return mapData;
    }
}


public struct MapData
{
    public GameObject mapPrefab;
    public GameObject batleTarget;

    public GameObject[] enemyPrefab;
    public int[] enemyCount;


    public void SetMapData(GameObject mapPrefab, GameObject[] enemyPrefab, int[] enemyCount)
    {
        this.mapPrefab = mapPrefab;
        //this.batleTarget = batleTarget;
        
        this.enemyPrefab = (GameObject[])enemyPrefab.Clone();
        this.enemyCount = (int[])enemyCount.Clone();
    }
}

