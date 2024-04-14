using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReactiveAnimation : MonoBehaviour
{
    [System.Serializable]
    public class ReactiveAnimationState
    {
        [HideInInspector]
        public ReactiveState ActiveState = ReactiveState.NONE;
        public float EaseInTime = 0.2f;
        [Tooltip("If true, EaseInTime will be based on the completion percentage of the previous state")]
        public bool RelativeEase = false;
        public Color TintColor = Color.white;

        [Space(4)]
        [Tooltip("This must be true to override the Affected Objects Settings")]
        public bool OverrideAffectedObjectsSettings = false;
        [Tooltip("If left empty, defaults to whatever the global setting is. If the global setting is also empty then it defaults to the transform of the object this component is on")]
        public List<Transform> AffectedParents = new List<Transform>();
        [Tooltip("Maximum depth of affected children, if set to -1 then the depth is uncapped and all children will be affected")]
        public int MaxEffectDepth = -1;
        public bool AffectsText = true;
    }

    [Header("Global Affected Objects Settings")]
    [Tooltip("If left empty, defaults to the transform of the object this component is on")]
    public List<Transform> AffectedParents = new List<Transform>();
    [Tooltip("Maximum depth of affected children, if set to -1 then the depth is uncapped and all children will be affected")]
    public int MaxEffectDepth = -1;
    public bool AffectsText = true;


    [HideInInspector]
    public List<ReactiveAnimationState> AnimationStates = new List<ReactiveAnimationState>();

    private Dictionary<ReactiveState, ReactiveAnimationState> m_States = new Dictionary<ReactiveState, ReactiveAnimationState>();

    private ReactiveAnimationState currState = null;

    private float timer = 0.0f;
    
    void Start()
    {
        // Initialize default values that need to be inited at runtime
        if(AffectedParents.Count == 0)
        {
            AffectedParents.Add(transform);
        }

        // Do the same for each animation state that overrides settings
        foreach(ReactiveAnimationState s in AnimationStates)
        {
            if(s.OverrideAffectedObjectsSettings)
            {
                s.AffectedParents = (s.AffectedParents.Count == 0 ? AffectedParents : s.AffectedParents);
            }

            m_States.Add(s.ActiveState, s);
        }

        ReactiveStateChanged(ReactiveState.NONE);
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
    
    void ReactiveStateChanged(ReactiveState newStateIndex)
    {
        if(m_States.ContainsKey(newStateIndex))
        {
            ReactiveAnimationState newState = m_States[newStateIndex];

            HandleNewState(newState);

            currState = newState;
        }
    }

    void HandleNewState(ReactiveAnimationState newState)
    {
        if(newState == null)
        {
            return;
        }

        if (currState == null || !currState.RelativeEase)
        {
            timer = 0.0f;
        }
        else
        {
            float percentage = Mathf.Min(timer / currState.EaseInTime, 1.0f);
            timer = timer >= currState.EaseInTime ? 0.0f : percentage * newState.EaseInTime;
        }

        bool overrides = newState.OverrideAffectedObjectsSettings;
        int maxDepth = (overrides ? newState.MaxEffectDepth : MaxEffectDepth);

        List<Transform> parents = overrides ? newState.AffectedParents : AffectedParents;

        //foreach(Transform t in parents)
        //{
        //    // Start our worker recursive function, start depth at 0
        //    UpdateParentAndChildrenRecursive(t, newState, maxDepth, 0);
        //}
    }

    public void ResetState()
    {
        HandleNewState(currState);
    }

    void UpdateParentAndChildrenRecursive(Transform t, ReactiveAnimationState state, int maxDepth, int depth)
    {
        if(maxDepth != -1 && depth > maxDepth)
        {
            return;
        }

        // Do tweening
        //TryTweenColor(t, state);

        // Update children
        for (int i = 0; i < t.childCount; ++i)
        {
            UpdateParentAndChildrenRecursive(t.GetChild(i), state, maxDepth, depth + 1);
        }
    }
}
