using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject TilePrefab = null;
    public int Width = 7;
    public int Height = 7;
    public Vector2 TileSize = Vector2.one;

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
        Vector2 startPos = new Vector2(TileSize.x * -0.5f * Width, 0.0f);

        for (int x = 0; x < Width; ++x)
        {
            tiles.Add(new List<GameObject>());
            Vector2 pos = startPos;

            for (int y = 0; y < Height; ++y)
            {
                pos.x += TileSize.x / 2.0f;
                pos.y += (TileSize.y / 2.0f);

                GameObject tile = Instantiate(TilePrefab, pos, Quaternion.identity, transform);
                tiles[x].Add(tile);
            }
            startPos.x += TileSize.x / 2.0f;
            startPos.y -= TileSize.y / 2.0f;
        }
    }

    public GameObject GetNearestTile(Vector2 pos)
    {
        return null;
    }
}
