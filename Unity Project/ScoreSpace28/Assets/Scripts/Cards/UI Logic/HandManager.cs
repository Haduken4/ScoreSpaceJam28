using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public int MaxHandSize = 10;

    public float MaxArcStrength = 0.2f;
    public float MaxArcAngle = 15.0f;
    public int MinCardsForArc = 5;
    public bool RelativeArcStrength = false;

    public float CardWidth = 200.0f;
    public float MaxCardSpacing = 10.0f;
    public float EmptySpotSpacing = 30.0f;

    public GameObject PositionParentPrefab = null;
    public GameObject TestCard = null;
    public int TestCardsToAdd = 0;
    public float TestCardTime = 1.0f;

    List<Transform> positionParents = new List<Transform>();

    List<Transform> cards = new List<Transform>();

    float timer = 0.0f;

    RectTransform rt = null;

    // Start is called before the first frame update
    void Start()
    {
        rt = (RectTransform)transform;

        if(PositionParentPrefab)
        {
            for(int i = 0; i < MaxHandSize; ++i)
            {
                Transform t = Instantiate(PositionParentPrefab, transform).GetComponent<Transform>();
                positionParents.Add(t);
            }
        }
        else
        {
            for (int i = 0; i < rt.childCount; ++i)
            {
                positionParents.Add(rt.GetChild(i));
            }
        }

        for(int i = 0; TestCard != null && i < TestCardsToAdd && TestCardTime == 0.0f; ++i)
        {
            cards.Add(Instantiate(TestCard, Vector3.zero, Quaternion.identity, positionParents[i]).transform);
        }
    }

    void TestCardUpdateLogic()
    {
        if (cards.Count < TestCardsToAdd)
        {
            timer += Time.deltaTime;
            if (timer >= TestCardTime)
            {
                cards.Add(Instantiate(TestCard, Vector3.zero, Quaternion.identity, transform).transform);
                timer = 0.0f;
            }
        }
    }

    void CardsUpdateLogic()
    {
        int emptySpots = 0;
        int cIndex = 0;
        List<int> spots = new List<int>();
        // Unparent all the cards since we need to readjust the parent positions
        foreach (Transform t in cards)
        {
            if (positionParents.Contains(t.parent))
            {
                t.SetParent(transform);
            }
            else
            {
                emptySpots = Mathf.Min(++emptySpots, 1);
                spots.Add(cIndex);
            }
            ++cIndex;
        }

        // Spacing between each card
        float spacing = Mathf.Min(rt.rect.width / cards.Count, CardWidth + MaxCardSpacing);
        // X position of first card
        float startX = -(((cards.Count - 1) / 2.0f) * spacing) - ((emptySpots * EmptySpotSpacing) / 2.0f);

        float emptySpacing = 0;
        // Calculate position and angle for each parent
        for (int i = 0; i < cards.Count; ++i)
        {
            Transform currT = positionParents[i];
            Vector3 newAngle = Vector3.zero;

            // Account for empty spacing in correct spots
            if (spots.Contains(i) || spots.Contains(i - 1))
            {
                // Weird ternary operator for edge case where the first card is hovered
                emptySpacing += EmptySpotSpacing / 2;
            }
            float x = startX + (spacing * i) + emptySpacing;
            float y = -rt.rect.height / 2.0f;

            if (cards.Count >= MinCardsForArc)
            {
                float multiplier = RelativeArcStrength ? ConvertFloatRange(cards.Count, 0, MaxHandSize, 0.0f, 1.0f) : 1.0f;

                float arcVal = ConvertFloatRange(x, -rt.rect.width / 2.0f, rt.rect.width / 2.0f, 0.0f, 1.0f);
                arcVal = ConvertFloatRange(1.0f - arcVal, 0.0f, 1.0f, 0.0f, Mathf.PI);

                newAngle.z = -MaxArcAngle * Mathf.Cos(arcVal) * multiplier;
                y += Mathf.Sin(arcVal) * multiplier * MaxArcStrength * rt.rect.height;
            }

            currT.eulerAngles = newAngle;
            currT.localPosition = new Vector3(x, y, 0);
        }

        //Update parent of each card (assuming we are allowed to, if a card is hovered/clicked then we can't change its parent)
        int pIndex = 0;
        foreach (Transform t in cards)
        {
            if (t.parent == transform)
            {
                t.SetParent(positionParents[pIndex]);
            }

            // Try to reset animation state to make sure we update our position smoothly instead of instantly
            t.GetComponent<ReactiveAnimation>()?.ResetState();

            ++pIndex;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TestCardUpdateLogic();

        // Remove any cards that no longer exist
        cards.RemoveAll(x => x == null);
        CardsUpdateLogic();
    }
    
    public void AddCard(Transform card)
    {
        cards.Add(card);
    }

    public void DiscardHand()
    {
        foreach(Transform t in cards)
        {
            // We can just destroy our hand on turn end
            if(t)
            {
                Destroy(t.gameObject);
            }
        }
    }

    // Converts val, a value in the range [min1, max1], to the equivalent value in the range [min2, max2] and returns it
    float ConvertFloatRange(float val, float min1, float max1, float min2, float max2)
    {
        return (((val - min1) / (max1 - min1)) * (max2 - min2)) + min2;
    }

    public int NumCardsInHand()
    {
        return cards.Count;
    }

    public List<GameObject> GetCardObjects()
    {
        List<GameObject> ret = new List<GameObject>();

        foreach(Transform c in cards)
        {
            ret.Add(c.gameObject);
        }

        return ret;
    }
}
