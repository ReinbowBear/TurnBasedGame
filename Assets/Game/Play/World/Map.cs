using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [HideInInspector] public MapPanel mapPanel;
    [HideInInspector] public MapData mapData;

    [HideInInspector] public byte[] index;

    public enum MapState
    {
        neutral,
        activate,
        deactive
    }
    [HideInInspector] public MapState mapState;

    public void ClickButton()
    {
        mapPanel.gameObject.SetActive(true);
        mapPanel.ShowMap(this);
    }


    public void ActivateMap()
    {
        mapState = MapState.activate;

        Image image = GetComponent<Image>();
        image.color = Color.green;
    }

    public void DeactivateMap()
    {
        mapState = MapState.deactive;

        Image image = GetComponent<Image>();
        image.color = Color.black;
    }
}
