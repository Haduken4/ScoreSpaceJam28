using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTurnEffect : MonoBehaviour
{
    protected PlantData pd = null;
    protected GridManager gm = null;

    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlantData>();
        gm = FindFirstObjectByType<GridManager>();
    }

    public virtual void OnEndOfTurn()
    {

    }
}
