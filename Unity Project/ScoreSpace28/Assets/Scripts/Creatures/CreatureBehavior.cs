using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehavior : MonoBehaviour
{
    protected TurnManager tm = null;

    protected virtual void Start()
    {
        tm = FindFirstObjectByType<TurnManager>();
    }

    public virtual void OnEndOfTurn()
    {

    }

    public virtual void SetSpawnerPlant(Transform plant)
    {

    }

    public virtual void UnParent()
    {
        
    }
}
