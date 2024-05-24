using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject TilePrefab = null;
    public int Width = 7;
    public int Height = 7;
    public Vector2 TileSize = Vector2.one;

    public float TooltipTime = 1.0f;
    public float WaveActivationTimer = 0.0f;

    [HideInInspector]
    public ReactiveTile HoveredTile = null;

    List<List<GameObject>> tiles = new List<List<GameObject>>();

    float tooltipTimer = 1.0f;
    bool tooltip = false;
    ReactiveTile tooltipTile = null;

    float waveTimer = 0.0f;
    float waveIndex = 0;

    bool finishedMakingGrid = false;

    void Awake()
    {
        GenerateGrid();
        if(WaveActivationTimer == 0.0f)
        {
            finishedMakingGrid = true;
        }
    }

    void Update()
    {
        if (HoveredTile && !tooltip)
        {
            tooltipTimer -= Time.deltaTime;
            if (tooltipTimer <= 0.0f)
            {
                tooltip = true;
                HoveredTile.GetComponent<TileLogic>().CreateTooltip();
            }
        }
        else if (HoveredTile == null)
        {
            tooltipTimer = TooltipTime;
        }
        else if (tooltip && HoveredTile != tooltipTile)
        {
            tooltipTile?.GetComponent<TileLogic>().DestroyTooltip();
            tooltip = false;
            tooltipTimer = TooltipTime;
        }

        if(WaveActivationTimer != 0.0f && waveIndex <= Width + Height - 2)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer >= WaveActivationTimer)
            {
                for(int x = 0; x <= waveIndex && x < Width; ++x)
                {
                    for(int y = 0; y <= waveIndex - x && y < Height; ++y)
                    {
                        if(x + y == waveIndex)
                        {
                            tiles[x][y].SetActive(true);
                            TileInitialAnimation tia = tiles[x][y].GetComponent<TileInitialAnimation>();
                            tia.Bottom += Vector3.forward * (y / 10.0f);
                            tia.Top += Vector3.forward * (y / 10.0f);
                            //tia.InitZ(y / 10.0f);
                        }
                    }
                }

                waveTimer = 0.0f;
                waveIndex++;

                if(waveIndex > Width + Height - 2)
                {
                    finishedMakingGrid = true;
                }
            }
        }
    }

    void GenerateGrid()
    {
        Vector3 tileScale = TileSize / new Vector2(2.0f, 1.0f);
        tileScale.z = 1;
        Vector2 startPos = new Vector2(TileSize.x * -0.7f * Width, 0.0f);

        for (int x = 0; x < Width; ++x)
        {
            tiles.Add(new List<GameObject>());
            Vector3 pos = startPos;

            for (int y = 0; y < Height; ++y)
            {
                pos.x += TileSize.x * 0.7f;
                pos.y += TileSize.y * 0.7f;
                pos.z = pos.y / 10.0f;

                Vector3 addPos = transform.position;

                GameObject tile = Instantiate(TilePrefab, pos + addPos, Quaternion.identity, transform);
                tile.transform.localScale = tileScale;
                tiles[x].Add(tile);
                if(WaveActivationTimer != 0.0f)
                {
                    tile.SetActive(false);
                }
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

    public List<GameObject> GetAdjacentTiles(GameObject tile)
    {
        int xCenter = 0;
        int yCenter = 0;

        for(int x = 0; x < tiles.Count; ++x)
        {
            List<GameObject> column = tiles[x];
            if(column.Contains(tile))
            {
                xCenter = x;
                yCenter = column.IndexOf(tile);
            }
        }

        List<GameObject> adjacents = new List<GameObject>();
        for(int x = xCenter - 1; x <= xCenter + 1; ++x)
        {
            if(x < 0 || x >= Width)
            {
                continue;
            }

            for(int y = yCenter - 1; y <= yCenter + 1; ++y)
            {
                if(y < 0 || y >= Height || (y == yCenter && x == xCenter))
                {
                    continue;
                }

                adjacents.Add(tiles[x][y]);
            }
        }

        return adjacents;
    }

    public List<GameObject> GetPlayableTiles(List<GameObject> cards)
    {
        List<GameObject> playableTiles = new List<GameObject>();
        List<PlantType> checkedTypes = new List<PlantType>();

        foreach (GameObject card in cards)
        {
            PlantType type = card.GetComponent<CardLogic>().CardType;
            if (checkedTypes.Contains(type))
            {
                continue;
            }

            for (int x = 0; x < Width; ++x)
            {
                for(int y = 0; y < Height; ++y)
                {
                    TileLogic tl = tiles[x][y].GetComponent<TileLogic>();
                    if (tl.CanPlant(type))
                    {
                        playableTiles.Add(tiles[x][y]);
                    }
                }
            }

            checkedTypes.Add(type);
        }

        return playableTiles;
    }

    public List<GameObject> GetPlayableTiles(GameObject card)
    {
        List<GameObject> playableTiles = new List<GameObject>();

        PlantType type = card.GetComponent<CardLogic>().CardType;

        for (int x = 0; x < Width; ++x)
        {
            for (int y = 0; y < Height; ++y)
            {
                TileLogic tl = tiles[x][y].GetComponent<TileLogic>();
                if (tl.CanPlant(type))
                {
                    playableTiles.Add(tiles[x][y]);
                }
            }
        }

        return playableTiles;
    }

    public void SetHoveredTile(ReactiveTile newTile)
    {
        if(HoveredTile != newTile)
        {
            if(HoveredTile)
            {
                HoveredTile.GetComponent<TileLogic>().DestroyTooltip();
            }

            HoveredTile = newTile;
            tooltipTimer = TooltipTime;
            tooltip = false;
        }
    }

    public bool DoneMakingGrid()
    {
        return finishedMakingGrid;
    }
}
