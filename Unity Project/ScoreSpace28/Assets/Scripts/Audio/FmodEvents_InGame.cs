using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents_InGame : MonoBehaviour
{

    [field: Header("Sound Effects")]
    [field: SerializeField] public EventReference PacketDraw { get; private set; }
    [field: SerializeField] public EventReference PlantSeedShuffle { get; private set; }
    [field: SerializeField] public EventReference BeeBuzzSound { get; private set; }

    [field: SerializeField] public EventReference VenusFlyChomps { get; private set; }



    //how to play sounds in other scripts
    //        AudioManager.instance.PlayOneShot(FMODEvents_InGame.instance."name", this.transform.position);

    //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/plantSFX/PlantSeedShuffle", GetComponent<Transform>().position);
    public static FMODEvents_InGame instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void StopSound()
    {

    }
}
