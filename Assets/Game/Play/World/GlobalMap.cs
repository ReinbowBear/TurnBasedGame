using System;
using UnityEngine;

public class GlobalMap : MonoBehaviour
{
    public byte pathCount;
    [Space]
    public byte mapHeight;
    public byte mapWidth;
    [SerializeField] private short offset;
    [Space]
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject PointerPrefab;
    [Space]
    public Transform[] mapsNumber;
    public Map[,,] mapPoints;
    public Map[,,] pathPoints;

    public System.Random random;

    public void NewGlobalMap()
    {        
        random = new System.Random(DateTime.Now.Millisecond);
        mapPoints = new Map[mapsNumber.Length, mapHeight, mapWidth];
        pathPoints = new Map[mapsNumber.Length, pathCount, mapHeight];

        for (byte i = 0; i < mapsNumber.Length; i++)
        {
            GeneratePoints(i, mapsNumber[i]);
        }

        for (byte i = 0; i < mapsNumber.Length; i++)
        {
            for (byte ii = 0; ii < pathCount; ii++)
            {
                GetRandomPath(i, ii);
            }
        }
    }


    private void GeneratePoints(byte pathId, Transform pathIdTransform)
    {
        for (byte i = 0; i < mapHeight; i++)
        {
            for (byte ii = 0; ii < mapWidth; ii++)
            {
                mapPoints[pathId, i, ii] = Instantiate(pointPrefab, pathIdTransform).GetComponent<Map>();
                mapPoints[pathId, i, ii].transform.position = new Vector3(pathIdTransform.position.x + ii*offset, pathIdTransform.position.y + i*offset, pathIdTransform.position.z);
                mapPoints[pathId, i, ii].name = $"Point {i} {ii}";
            }
        }
    }

    private void GetRandomPath(byte pathId, byte pathNumber)
    {
        for (byte i = 0; i < mapHeight; i++)
        {
            int randomID = random.Next(0, mapWidth);
            pathPoints[pathId, pathNumber, i] = mapPoints[pathId, i, randomID];
            
            mapPoints[pathId, i, randomID].gameObject.SetActive(true);
        }
        
        SetPointers(pathId, pathNumber);
    }

    private void SetPointers(byte pathId, byte pathNumber)
    {
        for (byte i = 0; i < mapHeight-1; i++)
        {
            RectTransform start = pathPoints[pathId, pathNumber, i].GetComponent<RectTransform>();
            RectTransform end = pathPoints[pathId, pathNumber, i+1].GetComponent<RectTransform>();

            MapPointer mapPointer = Instantiate(PointerPrefab, mapsNumber[pathId].transform).GetComponent<MapPointer>();
            mapPointer.SetTarget(start, end);
        }
    }


    private void ShowGlobalMap()
    {
        for (byte i = 0; i < mapsNumber.Length; i++)
        {
            mapsNumber[i].gameObject.SetActive(true);
        }
    }

    private void hideGlobalMap()
    {
        for (byte i = 0; i < mapsNumber.Length; i++)
        {
            mapsNumber[i].gameObject.SetActive(false);
        }
    }


    private void Save()
    {
        SaveGlobalMap saveGlobalMap = new SaveGlobalMap();

        saveGlobalMap.random = random;

        SaveSystem.gameData.saveGlobalMap = saveGlobalMap;
    }

    private void Load()
    {
        SaveGlobalMap saveGlobalMap = SaveSystem.gameData.saveGlobalMap;

        random = saveGlobalMap.random;
    }


    void OnEnable()
    {
        MapPanel.onNewBattle += hideGlobalMap;
        TurnManager.onEndLevel += ShowGlobalMap;

        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
    }

    void OnDisable()
    {
        MapPanel.onNewBattle -= hideGlobalMap;
        TurnManager.onEndLevel -= ShowGlobalMap;

        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }
}


public struct SaveGlobalMap
{
    public System.Random random;
}
