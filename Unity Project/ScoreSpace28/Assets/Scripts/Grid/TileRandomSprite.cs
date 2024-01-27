using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomSprite : MonoBehaviour
{
    public List<Sprite> SpriteVariations = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        if(SpriteVariations.Count != 0)
        {
            int randSprite = Random.Range(0, SpriteVariations.Count);
            GetComponent<SpriteRenderer>().sprite = SpriteVariations[randSprite];
        }
    }
}
