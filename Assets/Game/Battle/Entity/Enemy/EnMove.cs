using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;

public class EnMove : MonoBehaviour
{
    [SerializeField] private byte moveDistanse;
    private byte moveSpeed = 6;
    [Space]
    [SerializeField] private LayerMask defaultLayer;
    [SerializeField] private LayerMask rayLayer;
    [SerializeField] private GameObject EnemyModel;

    private Tile startTile;
    private List<Tile> closedSet = new List<Tile>();
    private List<Tile> openSet = new List<Tile>();
    private List<Tile> toCheck = new List<Tile>();


    public void PathCheck(List<PriorityStruct> costList)
    {
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
                
                PriorityStruct priorityData = new PriorityStruct();
                priorityData.tile = currentTile;
                costList.Add(priorityData);
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
    }


    public IEnumerator MoveTo(Tile targetTile)
    {
        startTile.isTaken = null;

        closedSet.Clear();
        List<Tile> path = PathFind(targetTile);

        targetTile.isTaken = gameObject;
        for (byte i = 0; i < path.Count; i++)
        {
            EnemyModel.transform.LookAt(path[i].transform.position);
            while (transform.position != path[i].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[i].transform.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
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


    public int FindCharacters()
    {
        List<float> distance = new List<float>();
        for (byte i = 0; i < GetCharacter.characterList.Count; i++)
        {
            float newDistance = Vector3.Distance(transform.position, GetCharacter.characterList[i].transform.position);
            distance.Add(newDistance);
        }
        float minValue = distance.Min();
        int minIndex = distance.IndexOf(minValue);


        List<float> tileDistance = new List<float>();
        for (byte i = 0; i < closedSet.Count; i++)
        {
            float newDistance = Vector3.Distance(closedSet[i].transform.position, GetCharacter.characterList[minIndex].transform.position);
            tileDistance.Add(newDistance);
        }
        minValue = tileDistance.Min();
        minIndex = tileDistance.IndexOf(minValue);
        return minIndex;
    }


    void OnMouseDown() //сделать функцию что покажет дальность хотьбы юнита
    {
        //PathCheck();
        foreach (Tile tile in closedSet)
        {
            tile.SweepMat(0);
        }
        BattleKeyboard.gameInput.Player.Mouse_0.started += FalsePath;
    }

    private void FalsePath(InputAction.CallbackContext context)
    {
        foreach (Tile tile in closedSet)
        {
            tile.DeactivateTile();
        }
        closedSet.Clear();
        BattleKeyboard.gameInput.Player.Mouse_0.started -= FalsePath;
    }
}
