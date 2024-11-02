using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacter : MonoBehaviour
{
    public static Action<GameObject> onMove;
    private Camera cam;

    [SerializeField] private LayerMask rayLayer;
    [SerializeField] private GameObject plModel;
    [Space]
    public int moveDistanse;
    private byte moveSpeed = 8;

    [HideInInspector] public Tile startTile;
    private List<Tile> openSet = new List<Tile>();
    private List<Tile> closedSet = new List<Tile>();
    private List<Tile> toCheck = new List<Tile>();

    [HideInInspector] public bool isMove;
    [HideInInspector] public bool isPathCheck; //переменная для подсветки персонажа и проверки надо ли проверять путь при клике на него
    [HideInInspector] public bool wasMoved;

    void Awake()
    {
        cam = Camera.main;
    }


    public void PathCheck()
    {
        isPathCheck = true;
        GameKeyboard.gameInput.Player.Mouse_0.started += StartMove;
        Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 1, rayLayer))
        {
            startTile = hit.transform.GetComponent<Tile>();
            foreach (Tile neighbor in startTile.neighbors)
            {
                openSet.Add(neighbor);
            }
        }

        for (byte i = 0; i < moveDistanse; i++)
        {
            foreach (Tile currentTile in openSet)
            {
                if (closedSet.Contains(currentTile) || currentTile.isTaken == true)
                {
                    continue;
                }

                foreach (Tile neighbor in currentTile.neighbors)
                {
                    if (!openSet.Contains(neighbor))
                    {
                        toCheck.Add(neighbor);
                    }
                }
                closedSet.Add(currentTile);
            }
            
            openSet.Clear();
            foreach (Tile tile in toCheck) //нельзя пополнять список в его же итерациях, так что создал переходник
            {
                openSet.Add(tile);
            }
            toCheck.Clear();
        }

        closedSet.Remove(startTile);
        openSet.Clear();

        foreach (Tile tile in closedSet)
        {
            tile.ActiveTile();
        }
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer))
        {
            if (hit.transform.CompareTag("ActiveTile"))
            {
                FalsePath(); //тут он сам и отписывается
                wasMoved = true;
                isPathCheck = false;
                StartCoroutine(MoveTo(hit.transform.GetComponent<Tile>()));
            }
        }
    }

    public IEnumerator MoveTo(Tile targetTile)
    {
        isMove = true;
        startTile.isTaken = null;
        onMove.Invoke(gameObject);

        List<Tile> path = PathFind(targetTile);
        targetTile.isTaken = gameObject;
        for (byte i = 0; i < path.Count; i++)
        {
            //Tween tween = target.DOMove(new Vector3(0, 10, 0), 2f);
            //yield return tween.WaitForCompletion();
            //plModel.
            transform.LookAt(path[i].transform.position);
            while (transform.position != path[i].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[i].transform.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        isMove = false;
    }

    private List<Tile> PathFind(Tile targetTile)
    {
        openSet.Add(startTile);
        while (openSet.Count > 0)
        {
            Tile currentTile = openSet[0];
            foreach (Tile tile in openSet)
            {
                if (tile.fullCost < currentTile.fullCost || tile.fullCost == currentTile.fullCost && tile.toTargetCost < currentTile.toTargetCost ) //находим самый выгодный тайл
                {
                    if (tile.isTaken != true)
                    {
                        currentTile = tile;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            closedSet.Add(currentTile);
            openSet.Remove(currentTile);

            if (currentTile == targetTile)
            {
                Tile currentPathTile = targetTile;
                List<Tile> path = new List<Tile>();

                while (currentPathTile != startTile)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.parent;
                }
                openSet.Clear();
                closedSet.Clear();

                path.Reverse();
                return path;
            }
            

            foreach (Tile neighbor in currentTile.neighbors)
            {
                if (closedSet.Contains(neighbor) || neighbor.isTaken == true)
                {
                    continue;
                }

                int costNeighbor = currentTile.toStartCost + currentTile.GetDistance(neighbor);
                if (costNeighbor < neighbor.toStartCost || !openSet.Contains(neighbor))
                {
                    neighbor.toStartCost = costNeighbor;
                    neighbor.parent = currentTile;

                    if (!openSet.Contains(neighbor))
                    {
                        neighbor.toTargetCost = neighbor.GetDistance(targetTile);
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }

    public void FalsePath()
    {
        GameKeyboard.gameInput.Player.Mouse_0.started -= StartMove;
        foreach (Tile tile in closedSet)
        {
            tile.DeactivateTile();
        }
        closedSet.Clear();
        isPathCheck = false;
    }
}
