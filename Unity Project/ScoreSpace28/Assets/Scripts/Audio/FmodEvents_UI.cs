using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodEvents_UI : MonoBehaviour
{
    // Start is called before the first frame update

    [field: Header("UI")]

    [field: SerializeField] public EventReference HoverSound { get; private set; }
    [field: SerializeField] public EventReference ConfirmSound { get; private set; }

    //how to play sounds in other scripts
    //        AudioManager.instance.PlayOneShot(FmodEvents_UI.instance."name", this.transform.position);

    //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/plantSFX/PlantSeedShuffle", GetComponent<Transform>().position);
    public static FmodEvents_UI instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
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
