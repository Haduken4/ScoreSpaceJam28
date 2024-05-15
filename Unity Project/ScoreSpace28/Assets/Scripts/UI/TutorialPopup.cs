using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    public GameObject ChildToActivate = null;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("SeenTutorial"))
        {
            FindFirstObjectByType<TurnManager>().TutorialPopup = false;
        }
        else
        {
            PlayerPrefs.SetInt("SeenTutorial", 1);
            ChildToActivate.SetActive(true);
        }
    }

    public void DeactivatePopup()
    {
        ChildToActivate.SetActive(false);
        FindFirstObjectByType<TurnManager>().TutorialPopup = false;
    }
}
