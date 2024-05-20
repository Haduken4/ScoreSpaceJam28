using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioOnLoad : MonoBehaviour
{
    public int MusicSwap = 0;
    public int MusicSet = 0;
    public int AmbienceSwap = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.SetMusicParameter(MusicSwap);
        AudioManager.instance.SetAmbienceParameter(AmbienceSwap);
        AudioManager.instance.SetMusicSetParameter(MusicSet);
    }
}
