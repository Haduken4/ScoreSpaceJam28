using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteToggleButton : MonoBehaviour
{
    public GameObject MutedCrossout = null;
    public AudioSource MusicSource = null;
    public AudioSource AmbientSource = null;

    bool muted = false;

    float musicVol = 0.0f;
    float ambientVol = 0.0f;

    private void Start()
    {
        if (MusicSource)
        {
            musicVol = MusicSource.volume;
        }

        if(AmbientSource)
        {
            ambientVol = AmbientSource.volume;
        }
    }

    public void ToggleMuted()
    {
        muted = !muted;

        MutedCrossout?.SetActive(muted);

        if(MusicSource)
        {
            MusicSource.volume = muted ? 0.0f : musicVol;
        }

        if(AmbientSource)
        {
            AmbientSource.volume = muted ? 0.0f : ambientVol;
        }
    }
}
