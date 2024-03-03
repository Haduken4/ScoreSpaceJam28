using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTileModifier : MonoBehaviour
{
    public string NameChange = "";
    public Sprite SpriteChange = null;
    public List<PlantType> AllowedPlantsChange = new List<PlantType>();
    public int ScoreOnModify = 0;
    public bool BecomePlant = false;

    [Space(5)]
    public bool ForceOverride = false;

    ScoreTextManager stm = null;

    // Start is called before the first frame update
    void Start()
    {
        stm = FindFirstObjectByType<ScoreTextManager>();
    }

    public void ModifyTile(TileLogic tl)
    {
        if(NameChange != "" || ForceOverride)
        {
            tl.gameObject.name = NameChange;
        }

        // Can't override this, we don't want to set the sprite to null ever
        if(SpriteChange != null)
        {
            tl.GetComponent<SpriteRenderer>().sprite = SpriteChange;
        }

        if(AllowedPlantsChange.Count != 0 || ForceOverride)
        {
            tl.AllowedPlants = AllowedPlantsChange;
        }

        tl.IsPlant = ForceOverride ? BecomePlant : (BecomePlant || tl.IsPlant);

        if(ScoreOnModify != 0)
        {
            Vector3 scorePos = tl.transform.position;
            scorePos.y += tl.transform.lossyScale.y / 2.0f;

            AddScore(ScoreOnModify, scorePos);
        }
    }

    public void AddScore(float val, Vector3 pos)
    {
        if (val == 0)
        {
            return;
        }

        GlobalGameData.Score += val;
        stm.SpawnScoreText(pos, val);
    }
}
