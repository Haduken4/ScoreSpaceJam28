using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public float Volume = 2.0f;
    public List<AudioClip> SoundVariations = new List<AudioClip>();

    public bool PlayOnStart = false;

    AudioSource source = null;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        if(PlayOnStart)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        source.volume = Volume * GlobalGameData.VolumeMultiplier;

        source.PlayOneShot(SoundVariations[Random.Range(0, SoundVariations.Count)]);
    }
}
