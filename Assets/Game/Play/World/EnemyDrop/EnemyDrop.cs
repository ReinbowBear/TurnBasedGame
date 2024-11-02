using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private bool isStartDrop;
    [SerializeField] private bool isRepeat;
    [Space]
    [SerializeField] private byte timeToDrop;
    [SerializeField] private byte dropCount;
    [Space]
    [SerializeField] private byte dropHeight;
    [SerializeField] private byte dropWidth;
    [Space]
    [SerializeField] private LayerMask rayLayer;
    private List<Tile> tiles = new List<Tile>();

    private BattleMapManager battleMapManager;
    private System.Random random;
    private byte turnTime;

    void Awake()
    {
        battleMapManager = transform.root.GetComponent<BattleMapManager>();
        random = new System.Random(DateTime.Now.Millisecond);
    }

    void Start()
    {
        if (isStartDrop)
        {
            PrepareTiles();
            DropEnemy();
        }
    }


    private void TurnTimer()
    {
        turnTime++;
        if (turnTime == timeToDrop-1)
        {
            PrepareTiles();
        }
        else if (turnTime == timeToDrop)
        {
            DropEnemy();  

            if (!isRepeat)
            {
                gameObject.SetActive(false);
            }
            else
            {
                turnTime = 0;
            }
        }
    }

    private void PrepareTiles()
    {
        for (byte i = 0; i < dropHeight; i++)
        {
            for (byte ii = 0; ii < dropWidth; ii++)
            {
                Ray ray = new Ray(transform.position + new Vector3(i, 0.5f, ii), Vector3.down);
                if (Physics.Raycast(ray, out RaycastHit hit, 5, rayLayer))
                {
                    tiles.Add(hit.transform.GetComponent<Tile>());
                }
            }
        }

        foreach (var tile in tiles)
        {
            if (tile.isTaken == null)
            {
                tile.SweepMat(2);
            }
        }
    }

    private void DropEnemy()
    {
        for (byte i = 0; i < dropCount; i++)
        {
            if (battleMapManager.mapEnemys.Count == 0)
            {
                break;
            }

            bool wasSpawned = false;
            while (wasSpawned == false)
            {
                if (tiles.Count == 0)
                {
                    break;
                }

                int randomIndex = random.Next(0, tiles.Count);

                if (tiles[randomIndex].isTaken != true)
                {
                    wasSpawned = true;
                    Instantiate(battleMapManager.mapEnemys[0], tiles[randomIndex].transform.position, Quaternion.identity);     
                    battleMapManager.mapEnemys.Remove(battleMapManager.mapEnemys[0]);         
                }
                else
                {
                    tiles[randomIndex].DeactivateTile();
                    tiles.Remove(tiles[randomIndex]);
                }
            }
        }

        foreach (var tile in tiles)
        {
            tile.DeactivateTile();
        }
        tiles.Clear();
    }


    void OnEnable()
    {
        TurnManager.onPlayerTurn += TurnTimer;
    }

    void OnDisable()
    {
        TurnManager.onPlayerTurn -= TurnTimer;
    }
}
