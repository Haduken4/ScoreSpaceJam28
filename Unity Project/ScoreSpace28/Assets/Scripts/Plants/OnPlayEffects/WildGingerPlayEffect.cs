using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildGingerPlayEffect : OnPlayEffect
{
    public PlantType SubtractType = PlantType.ROOT;
    public float SubtractValue = 2;

    public List<GameObject> AddableCards = new List<GameObject>();
    public int CardsToAdd = 0;

    protected override void GameplayEffect()
    {
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);

        foreach (GameObject obj in neighbors)
        {
            TileLogic tl = obj.GetComponent<TileLogic>();
            Transform plant = tl.GetPlant();

            if (plant && plant.GetComponent<PlantData>().Type == SubtractType)
            {
                pd.AddScoreNoDisplay(SubtractValue);
                plant.GetComponent<PlantData>().DisplayScore(Mathf.Floor(SubtractValue));
            }
        }

        HandManager hm = FindFirstObjectByType<HandManager>();
        for (int i = 0; i < CardsToAdd; ++i)
        {
            GameObject card = Instantiate(AddableCards[Random.Range(0, AddableCards.Count)], hm.transform);
            card.transform.position = transform.position;
            card.transform.localScale *= 0.1f;

            hm.AddCard(card.transform);
        }
    }
}
