using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reactive : MonoBehaviour
{
    protected ReactiveManager rm = null;

    [HideInInspector]
    public ReactiveState CurrState = ReactiveState.NONE;

    protected virtual void Start()
    {
        rm = FindFirstObjectByType<ReactiveManager>();

        //BroadcastMessage("ReactiveStateChanged", CurrState, SendMessageOptions.DontRequireReceiver);
    }

    protected virtual void OnMouseEnter()
    {
        rm.ObjectHovered(this);
    }

    protected virtual void OnMouseExit()
    {
        rm.ObjectUnhovered(this);
    }

    protected virtual void OnMouseDown()
    {

    }

    protected virtual void ReactiveStateChanged(ReactiveState newState)
    {
        CurrState = newState;
    }
}
