using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicOnLoad : MonoBehaviour
{
    public int MusicSwap = 1;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.SetMusicParameter(MusicSwap);
    }
}
