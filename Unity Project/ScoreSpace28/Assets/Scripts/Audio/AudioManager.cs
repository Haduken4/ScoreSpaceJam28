using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    
    
    private List<EventInstance> eventInstances = new List<EventInstance>();

    private EventInstance ambienceEventInstance;

    private EventInstance musicEventInstance;

 
    public static AudioManager instance { get; private set; }

    [field: Header("Music")]

    [field: SerializeField] public EventReference music { get; private set; }
    public string MusicSwitchParameter = "";


    [field: Header("Ambience")]

    [field: SerializeField] public EventReference ambience { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeAmbience(ambience);
        InitializeMusic(music);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    public void SetMusicParameter(int value)
    {
        musicEventInstance.setParameterByName(MusicSwitchParameter, value);
    }

    public void PlayOneShot(EventReference sound, Vector2 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }


    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicEventInstance.release();

        ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ambienceEventInstance.release();
    }
    private void OnDestroy()
    {
        CleanUp();
    }
}
