using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPollinateEffect : MonoBehaviour
{
    protected PlantData pd = null;

    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlantData>();
    }

    public virtual void PollinateEffect()
    {

    }
}
