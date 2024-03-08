using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReactiveCard : Reactive, IPointerEnterHandler, IPointerExitHandler
{
    public static Transform HoveredParent = null;
    public static Transform ClickedParent = null;

    Transform lastParent = null;
    Transform currParent = null;

    TurnManager tm = null;
    CardLogic cl = null;

    //SoundPlayer sp = null;

    protected override void Start()
    {
        base.Start();

        tm = FindFirstObjectByType<TurnManager>();
        cl = GetComponent<CardLogic>();
        //sp = GetComponent<SoundPlayer>();

        if (!ClickedParent || !HoveredParent)
        {
            ClickedParent = GameObject.Find("ClickedParent").transform;
            HoveredParent = GameObject.Find("HoveredParent").transform;
        }
    }

    protected override void ReactiveStateChanged(ReactiveState newState)
    {
        ReactiveState lastState = CurrState;
        if(lastState == ReactiveState.L_CLICKED && cl.InPlayArea())
        {
            FollowCursor fc = FindFirstObjectByType<FollowCursor>();
            if(fc.GetTrash() != null && tm.CanTrash())
            {
                Destroy(gameObject);
                tm.CardTrashed();
                fc.GetTrash().CloseLid();
                return;
            }

            if(cl.PlayCard())
            {
                Debug.Log("Destroying card");
                Destroy(gameObject);
                return;
            }
        }

        base.ReactiveStateChanged(newState);

        if(newState == ReactiveState.NONE)
        {
            HandleNoneState();
        }
        else if(newState == ReactiveState.HOVERED || newState == ReactiveState.R_CLICKED)
        {
            HandleHoveredState();
        }
        else if(newState == ReactiveState.L_CLICKED)
        {
            HandleClickedState();
        }
    }

    void HandleNoneState()
    {
        // Go back to the hand if we weren't taken by something unknown
        if (transform.parent == HoveredParent || transform.parent == ClickedParent)
        {
            transform.SetParent(lastParent, true);
            rm.ObjectUnhovered(this);
        }
    }

    void HandleHoveredState()
    {
        // Player let go of card, we could be played or return to hand depending on where - either way return to hand parent
        if (transform.parent == ClickedParent)
        {
            transform.SetParent(lastParent, true);
            return;
        }
    }

    void HandleClickedState()
    {
        // Make sure we preserve the hand parent
        if(transform.parent == HoveredParent)
        {
            currParent = lastParent;
        }
        transform.SetParent(ClickedParent, true);
    }

    private void OnTransformParentChanged()
    {
        lastParent = currParent;
        currParent = transform.parent;
    }

    public void UnhoverCard()
    {
        if(CurrState != ReactiveState.L_CLICKED)
        {
            ReactiveStateChanged(ReactiveState.NONE);
        }
    }

    public void BecomeHoveredCard()
    {
        if (HoveredParent.childCount == 1)
        {
            HoveredParent.GetChild(0).GetComponent<ReactiveCard>().UnhoverCard();
        }
        Vector3 hoverPos = transform.parent.position;
        hoverPos.y = transform.parent.parent.position.y;
        HoveredParent.position = hoverPos;
        transform.SetParent(HoveredParent, true);
        ReactiveStateChanged(ReactiveState.HOVERED);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //playsoundHere
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PlantSeedShuffle, transform.position);
        //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/plantSFX/PlantSeedShuffle", GetComponent<Transform>().position);

        if (ClickedParent.childCount == 1)
        {
            return;
        }
        rm.ObjectHovered(this);
        BecomeHoveredCard();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ClickedParent.childCount == 1)
        {
            return;
        }
        rm.ObjectUnhovered(this);
        UnhoverCard();
    }

    protected override void OnMouseEnter()
    {
        //base.OnMouseEnter();
    }

    protected override void OnMouseExit()
    {
        //base.OnMouseExit();
    }
}
