using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTurnEffect : MonoBehaviour
{
    protected PlantData pd = null;
    protected GridManager gm = null;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pd = GetComponent<PlantData>();
        gm = FindFirstObjectByType<GridManager>();
    }

    public virtual void OnEndOfTurn()
    {
        Debug.Log("End Of Turn Effect Not Implemented: " + gameObject.name);
    }
}
