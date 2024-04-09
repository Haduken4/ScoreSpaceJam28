using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MuteToggleButton : MonoBehaviour
{
    public GameObject MutedCrossout = null;

    static bool muted = false;

    Bus masterBus;

    private void Awake()
    {
        masterBus = RuntimeManager.GetBus("bus:/");
    }

    private void Start()
    {
        MutedCrossout?.SetActive(muted);
    }

    public void ToggleMuted()
    {
        muted = !muted;

        MutedCrossout?.SetActive(muted);

        masterBus.setVolume(muted ? 0 : 1);
    }
}
