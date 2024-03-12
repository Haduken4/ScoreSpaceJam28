using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public string SceneToSwap = "BarScene";    

    public void ClickButton()
    {
        if(SceneToSwap == "Quit")
        {
            Application.Quit();
            return;
        }
        SceneManager.LoadScene(SceneToSwap);

        AudioManager.instance.PlayOneShot(FmodEvents_UI.instance.ConfirmSound, this.transform.position);
    }
    public void PlayHover()
    {
        AudioManager.instance.PlayOneShot(FmodEvents_UI.instance.HoverSound, this.transform.position);
    }

    public void MusicSwitch()
    {
        AudioManager.instance.SetMusicParameter("MusicSwitch", 1);
    }

}
