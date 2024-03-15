using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MuteToggleButton : MonoBehaviour
{
    public GameObject MutedCrossout = null;

    bool muted = false;

    Bus masterBus;

    private void Awake()
    {
        masterBus = RuntimeManager.GetBus("bus:/");
    }

    public void ToggleMuted()
    {
        muted = !muted;

        MutedCrossout?.SetActive(muted);

        masterBus.setMute(muted);
    }
}
