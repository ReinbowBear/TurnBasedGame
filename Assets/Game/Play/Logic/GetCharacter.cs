using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacter : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameCanvas gameCanvas;
    [SerializeField] private ReadCharacter readCharacter;
    [SerializeField] private ClickCharacter clickController;
    [Space]
    [SerializeField] private LayerMask rayLayer;
    [SerializeField] private byte tilesCount;
    [Space]
    [SerializeField] private GameObject[] characters;
    public static List<GameObject> CharacterList = new List<GameObject>();
    private List<Tile> tileList = new List<Tile>();


    private void NewDrop()
    {
        for (byte i = 0; i < readCharacter.characterSlots.Length; i++)
        {
            if (readCharacter.characterSlots[i].item != null)
            {
                GameObject newCharacter = Instantiate(readCharacter.characterSlots[i].item.itemSO.itemPrefab, transform.position + new Vector3 (0, -20, 0), Quaternion.identity);
                CharacterList.Add(newCharacter);
            }
        }
        StartCoroutine(DropZone()); //может запустить функцию даже если персонажей 0
    }


    private IEnumerator DropZone() //запускается по событию и по идеи может пропустить тайлы, но это не происходит из-за очереди подписок (это может сломатся)
    {
        for (byte i = 0; i < tilesCount; i++)
        {
            Ray ray = new Ray(transform.position + new Vector3(-1, -0.2f, i), transform.TransformDirection(1, 0, 0));
            RaycastHit[] rayHits = Physics.RaycastAll(ray, 50, rayLayer);
    
            for (byte x = 0; x < rayHits.Length; x++)
            {
                Tile tileScript = rayHits[x].transform.GetComponent<Tile>();
                if (tileScript.isTaken != true)
                {
                    tileList.Add(tileScript);
                    tileScript.ActiveTile(); 
                }
            }
        }

        byte charactersLeft = 0;
        while (charactersLeft != CharacterList.Count)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer))
            {
                if (hit.transform.CompareTag("ActiveTile"))
                {
                    CharacterList[charactersLeft].transform.position = hit.transform.position;
                    if (GameKeyboard.gameInput.Player.Mouse_0.triggered)
                    {
                        Tile tileScript = hit.transform.GetComponent<Tile>();
                        tileScript.isTaken = CharacterList[charactersLeft];
                        tileScript.DeactivateTile();

                        Rigidbody rigidbody = CharacterList[charactersLeft].GetComponent<Rigidbody>();
                        rigidbody.velocity = Vector3.zero;
                        rigidbody.angularVelocity = Vector3.zero; // Убираем вращение
                        charactersLeft++;
                    }
                }
            }
            yield return null;
        }

        foreach (Tile tileScript in tileList)
        {
            tileScript.DeactivateTile();
        }
        tileList.Clear();
        yield return null;
        gameCanvas.ShowUI();
        clickController.enabled = true;
    }


    void OnEnable()
    {
        MapPanel.onNewBattle += NewDrop;
    }

    void OnDisable()
    {
        MapPanel.onNewBattle -= NewDrop;
    }
}
