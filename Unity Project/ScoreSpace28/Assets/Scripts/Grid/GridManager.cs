using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject TilePrefab = null;
    public int Width = 7;
    public int Height = 7;
    public Vector2 TileSize = Vector2.one;

    [HideInInspector]
    public ReactiveTile HoveredTile = null;

    List<List<GameObject>> tiles = new List<List<GameObject>>();

    void Awake()
    {
        GenerateGrid();
    }

    void Update()
    {
        
    }

    void GenerateGrid()
    {
        Vector3 tileScale = TileSize / new Vector2(2.0f, 1.0f);
        tileScale.z = 1;
        Vector2 startPos = new Vector2(TileSize.x * -0.7f * Width, 0.0f);

        for (int x = 0; x < Width; ++x)
        {
            tiles.Add(new List<GameObject>());
            Vector2 pos = startPos;

            for (int y = 0; y < Height; ++y)
            {
                pos.x += TileSize.x * 0.7f;
                pos.y += TileSize.y * 0.7f;

                GameObject tile = Instantiate(TilePrefab, pos, Quaternion.identity, transform);
                tile.transform.localScale = tileScale;
                tiles[x].Add(tile);
            }
            startPos.x += TileSize.x * 0.7f;
            startPos.y -= TileSize.y * 0.7f;
        }


        // SORT Z
        foreach(List<GameObject> tilesColumn in tiles)
        {
            foreach(GameObject tile in tilesColumn)
            {
                Vector3 pos = tile.transform.localPosition;
                pos.z = pos.y / 10.0f;
            }
        }    
    }

    public GameObject GetNearestTile(Vector2 pos)
    {
        return null;
    }
}
