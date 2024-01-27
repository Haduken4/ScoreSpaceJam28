using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReactiveState { NONE, HOVERED, L_CLICKED, R_CLICKED, SELECTED, DISABLED }

public class ReactiveManager : MonoBehaviour
{
    Reactive HoveredObject = null;
    Reactive ClickedObject = null;

    public void ObjectClicked(Reactive obj)
    {
        ClickedObject = obj;
    }

    public void ObjectHovered(Reactive obj)
    {
        HoveredObject = obj;
        UpdateObjectState(ReactiveState.HOVERED, HoveredObject);
    }

    public void ObjectUnhovered(Reactive obj)
    {
        if(HoveredObject == obj)
        {
            HoveredObject = null;
        }
        UpdateObjectState(ReactiveState.NONE, obj);
    }

    private void Update()
    {
        // When an object is clicked, we want to treat it as a force hover - we will wait to see if the user releases the button
        if (HoveredObject)
        {
            HandleLeftClick();
            HandleRightClick();
        }
        
    }

    void HandleLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickedObject = HoveredObject;
            UpdateObjectState(ReactiveState.L_CLICKED, ClickedObject);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(HoveredObject == ClickedObject)
            {
                UpdateObjectState(ReactiveState.HOVERED, ClickedObject);
            }
            else
            {
                UpdateObjectState(ReactiveState.NONE, ClickedObject);
            }
            ClickedObject = null;
        }
    }

    void HandleRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UpdateObjectState(ReactiveState.R_CLICKED, ClickedObject);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (HoveredObject == ClickedObject)
            {
                UpdateObjectState(ReactiveState.HOVERED, ClickedObject);
            }
            else
            {
                UpdateObjectState(ReactiveState.NONE, ClickedObject);
            }
            ClickedObject = null;
        }
    }

    void UpdateObjectState(ReactiveState newState, Reactive r = null)
    {
        if (r != null) 
        {
            r.BroadcastMessage("ReactiveStateChanged", newState, SendMessageOptions.DontRequireReceiver);
        }
    }
}
