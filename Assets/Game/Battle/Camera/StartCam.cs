using UnityEngine;

public class StartCam : MonoBehaviour
{
    [SerializeField] private GetCharacter getCharacter;

    public void SetCam() //запускается по событию и по идеи может может сработать а список будет пустым, но это не происходит из-за очереди подписок (это может сломатся)
    {
        float offsetX = getCharacter.tileList.Count/getCharacter.tilesCount /2;
        float offsetZ = getCharacter.tilesCount/2 -0.5f;
        transform.position = new Vector3(offsetX, 0, offsetZ);
    }
}
