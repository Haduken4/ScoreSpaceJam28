using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckRandomBool
{
    public DeckRandomBool(int deckSize, int numTrue)
    {
        mainDeck = new List<bool>();
        for (int i = 0; i < numTrue; ++i)
        {
            mainDeck.Add(true);
        }
        for (int i = numTrue; i < deckSize; ++i)
        {
            mainDeck.Add(false);
        }

        Shuffle();

        discard = new List<bool>();
    }

    public bool Draw()
    {
        if (mainDeck.Count == 0)
        {
            ReshuffleDiscard();
        }

        if (mainDeck.Count == 0)
        {
            return false;
        }

        bool value = mainDeck[mainDeck.Count - 1];
        mainDeck.RemoveAt(mainDeck.Count - 1);
        discard.Add(value);
        return value;
    }

    private void Shuffle()
    {
        int i = mainDeck.Count;
        while (i > 1)
        {
            --i;
            int j = Random.Range(0, i + 1);
            bool temp = mainDeck[j];
            mainDeck[j] = mainDeck[i];
            mainDeck[i] = temp;
        }
    }

    public void ReshuffleDiscard()
    {
        mainDeck.AddRange(discard);
        Shuffle();
        discard.Clear();
    }

    private List<bool> mainDeck;
    private List<bool> discard;
}

public class DeckRandom<T>
{
    public void AddOption(T option, int numToAdd)
    {
        for (int i = 0; i < numToAdd; ++i)
        {
            mainDeck.Add(option);
        }
        Shuffle(mainDeck);
    }

    public void AddOptions(List<T> options)
    {
        mainDeck.AddRange(options);
        Shuffle(mainDeck);
    }

    public void AddOptionToDiscard(T option, int numToAdd = 1)
    {
        for(int i = 0; i < numToAdd; ++i)
        {
            discard.Add(option);
        }
    }

    public void AddOptionsToDiscard(List<T> options)
    {
        discard.AddRange(options);
    }

    public T Draw()
    {
        if (mainDeck.Count == 0)
        {
            ReshuffleDiscard();
        }

        if (mainDeck.Count == 0)
        {
            if(EnableAssert)
            {
                throw new System.Exception("Attempted to draw from a deck which never had cards added to it");
            }
            else
            {
                return default(T);
            }
        }

        T value = mainDeck[mainDeck.Count - 1];
        mainDeck.RemoveAt(mainDeck.Count - 1);
        if(DiscardOnDraw)
        {
            discard.Add(value);
        }
        return value;
    }

    public List<T> DrawX(int x)
    {
        List<T> drawn = new List<T>();

        for (int i = 0; i < x; ++i)
        {
            if (mainDeck.Count == 0)
            {
                ReshuffleDiscard();
            }

            if (mainDeck.Count == 0)
            {
                break;
            }

            T value = mainDeck[mainDeck.Count - 1];
            mainDeck.RemoveAt(mainDeck.Count - 1);
            drawn.Add(value);
        }

        if(DiscardOnDraw)
        {
            discard.AddRange(drawn);
        }
        return drawn;
    }

    public int NumCards()
    {
        return mainDeck.Count + discard.Count;
    }

    public List<T> GetUniqueValues()
    {
        return mainDeck.Select(x => x).Distinct().ToList();
    }

    public void Shuffle(List<T> toShuffle)
    {
        int i = toShuffle.Count;
        while (i > 1)
        {
            --i;
            int j = Random.Range(0, i + 1);
            T temp = toShuffle[j];
            toShuffle[j] = toShuffle[i];
            toShuffle[i] = temp;
        }
    }

    public void RemoveCopies(T value)
    {
        while (mainDeck.Remove(value)) { }
    }

    public void ReshuffleDiscard()
    {
        mainDeck.AddRange(discard);
        Shuffle(mainDeck);
        discard.Clear();
    }

    public List<T> GetDeck()
    {
        return mainDeck;
    }

    public List<T> GetDiscard()
    {
        return discard;
    }

    private List<T> mainDeck = new List<T>();
    private List<T> discard = new List<T>();

    public bool DiscardOnDraw = true;
    public bool EnableAssert = true;
}


public class RandomUtility
{

}
