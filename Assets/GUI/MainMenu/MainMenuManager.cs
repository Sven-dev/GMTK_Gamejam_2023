using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private bool Clicked = false;

    public void OnStartButton()
    {
        if (!Clicked)
        {
            Clicked = true;
            LevelManager.Instance.LoadLevel(2, Transition.Heart);
            AudioManager.Instance.FadeOut("Main Menu", 2f);
        }
    }

    public void OnControlsButton()
    {

    }

    public void OnOptionsbutton()
    {
        
    }

    public void OnCreditsButton()
    {
        if (!Clicked)
        {
            Clicked = true;
            AudioManager.Instance.FadeOut("Main Menu", 1f);
            LevelManager.Instance.LoadLevel(5, Transition.Heart);
        }
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
