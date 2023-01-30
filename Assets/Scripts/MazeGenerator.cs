using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Properties")]
    public Transform tileParent;
    public int width = 5;
    public int depth = 5;
    public GameObject startTile;
    public GameObject goalTile;
    public GameObject[] tileArray;
    public List<GameObject> tileList;
    public GameObject playerPrefab;
    public GameObject player;

    

    void Awake()
    {
        startTile = Resources.Load<GameObject>("Prefabs/StartTile");
        goalTile = Resources.Load<GameObject>("Prefabs/GoalTile");

        tileArray = new GameObject[5];
        tileArray[0] = Resources.Load<GameObject>("Prefabs/Tile1");
        tileArray[1] = Resources.Load<GameObject>("Prefabs/Tile2");
        tileArray[2] = Resources.Load<GameObject>("Prefabs/Tile3");
        tileArray[3] = Resources.Load<GameObject>("Prefabs/Tile4");
        tileArray[4] = Resources.Load<GameObject>("Prefabs/Tile5");

        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        tileList = new List<GameObject>();
    }

    void Start()
    {
        tileParent = GameObject.Find("[TILES]").transform;
        BuildTileList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyTiles();
            DestroyPlayer();
            BuildTileList();
        }
    }


    private void BuildTileList()
    {
        for (var row = 0; row < depth; row++)
        {
            for (var col = 0; col < width; col++)
            {
                var tempTile = Instantiate(tileArray[Random.Range(0,5)], new Vector3(col * 5.0f, 0.0f, row * 5.0f), 
                                Quaternion.Euler(0.0f, Random.Range(1,4) * 90.0f, 0.0f), tileParent);
                tileList.Add(tempTile);
            }
        }
        // set the start tile
        var startTileIndex = Random.Range(0, width);
        var startTilePosition = tileList[startTileIndex].transform.position;
        Destroy(tileList[startTileIndex]);
        tileList[startTileIndex] = Instantiate(startTile, startTilePosition, Quaternion.identity, tileParent);

        // add the player
        AddPlayer(tileList[startTileIndex].transform.position);

        // set the goal tile
        var goalTileIndex = (width * depth) - Random.Range(0, width) - 1;
        var goalTilePosition = tileList[goalTileIndex].transform.position;
        Destroy(tileList[goalTileIndex]);
        tileList[goalTileIndex] = Instantiate(goalTile, goalTilePosition, Quaternion.identity, tileParent);
    }

    private void DestroyTiles()
    {
        foreach (var tile in tileList)
        {
            Destroy(tile);
        }
        tileList.Clear();
    }

    private void AddPlayer(Vector3 position)
    {
        player = Instantiate(playerPrefab, new Vector3(position.x, 10.0f ,position.y), Quaternion.identity);
    }

    private void DestroyPlayer()
    {
        Destroy(player);
    }
}
