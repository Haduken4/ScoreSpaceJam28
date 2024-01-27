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

    }

    public GameObject GetNearestTile(Vector2 pos)
    {
        return null;
    }
}
