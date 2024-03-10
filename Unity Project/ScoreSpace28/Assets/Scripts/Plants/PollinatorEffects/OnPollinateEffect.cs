using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnPollinateEffect : MonoBehaviour
{
    protected OnPlayEffect ope = null;

    // Start is called before the first frame update
    void Start()
    {
        ope = GetComponent<OnPlayEffect>();
    }

    public abstract void PollinateEffect();
}
