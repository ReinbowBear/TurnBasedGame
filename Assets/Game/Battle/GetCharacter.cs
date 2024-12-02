using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacter : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameCanvas gameCanvas;
    [SerializeField] private ClickCharacter clickController;
    [SerializeField] private AbilitySlot[] characterSlots;
    [Space]
    public byte tilesCount;
    [SerializeField] private LayerMask rayLayer;

    public static List<GameObject> characterList = new List<GameObject>();
    [HideInInspector] public List<Tile> tileList = new List<Tile>();

    public void NewDrop()
    {
        for (byte i = 0; i < characterSlots.Length; i++)
        {
            if (characterSlots[i].item != null)
            {
                Debug.Log("новая система базі контента, надо тут переписать");
                //GameObject newCharacter = Instantiate(characterSlots[i].item.itemSO.itemPrefab, transform);
                //newCharacter.transform.position += new Vector3 (0, -20, 0);
                //characterList.Add(newCharacter);
            }
        }
        StartCoroutine(DropZone()); //может запустить функцию даже если персонажей 0
    }


    private IEnumerator DropZone()
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
        while (charactersLeft != characterList.Count)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer))
            {
                if (hit.transform.CompareTag("ActiveTile"))
                {
                    characterList[charactersLeft].transform.position = hit.transform.position;
                    if (BattleKeyboard.gameInput.Player.Mouse_0.triggered)
                    {
                        Tile tileScript = hit.transform.GetComponent<Tile>();
                        tileScript.isTaken = characterList[charactersLeft];
                        tileScript.DeactivateTile();

                        Rigidbody rigidbody = characterList[charactersLeft].GetComponent<Rigidbody>();
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
}
