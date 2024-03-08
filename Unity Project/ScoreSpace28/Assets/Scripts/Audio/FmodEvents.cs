using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field: Header("Sound Effects")]
    [field: SerializeField] public EventReference PacketDraw { get; private set; }
    [field: SerializeField] public EventReference PlantSeedShuffle { get; private set; }
    [field: SerializeField] public EventReference BeeBuzzSound { get; private set; }

    [field: SerializeField] public EventReference VenusFlyChomps { get; private set; }

    //how to play sounds in other scripts
    //        AudioManager.instance.PlayOneShot(FMODEvents.instance."name", this.transform.position);

    //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/plantSFX/PlantSeedShuffle", GetComponent<Transform>().position);
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void StopSound()
    {

    }
}
