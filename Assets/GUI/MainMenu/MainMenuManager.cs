using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private UnityEngine.UI.Button ControlsButton;

    private bool Clicked = false;

    public void OnStartButton()
    {
        if (!Clicked)
        {
            Clicked = true;
            LevelManager.Instance.LoadLevel(2, Transition.Crossfade);
        }
    }

    public void OnTestbutton()
    {
        LevelManager.Instance.LoadLevel(4, Transition.Crossfade);
        AudioManager.Instance.FadeOut("Main Menu", 1f);
    }

    public void OnControlsButton()
    {
        ControlsPanel.SetActive(true);
        ControlsButton.Select();
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
            LevelManager.Instance.LoadLevel(3, Transition.Crossfade);
        }
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}