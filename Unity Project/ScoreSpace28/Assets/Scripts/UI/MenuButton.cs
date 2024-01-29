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
    }
}
