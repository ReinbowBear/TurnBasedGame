using UnityEngine;

public class GlobalMapManager : MonoBehaviour
{
    [SerializeField] private GlobalMap globalMap;
    [SerializeField] private MapPanel mapPanel;

    public void StartLayer()
    {
        for (byte i = 0; i < globalMap.mapsNumber.Length; i++)
        {
            for (byte ii = 0; ii < globalMap.pathCount; ii++)
            {
                globalMap.pathPoints[i, ii, 0].ActivateMap();
            }
        }
    }


    public void NewLayer()
    {
        Map map = mapPanel.chosenMap;
        
        map.DeactivateMap();

        if (map.index[2] != globalMap.mapHeight-1)
        {
            Map newMap = globalMap.pathPoints[map.index[0], map.index[1], map.index[2]+1];
            if (newMap.mapState == Map.MapState.neutral)
            {
                newMap.ActivateMap();
            }
        }
    }


    void OnEnable()
    {
        MapPanel.onNewBattle += NewLayer;
    }

    void OnDisable()
    {
        MapPanel.onNewBattle -= NewLayer;
    }
}
