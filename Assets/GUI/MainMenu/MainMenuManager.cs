using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButton()
    {
        LevelManager.Instance.LoadLevel(2, Transition.Heart);
        AudioManager.Instance.FadeOut("Main Menu", 2f);
    }

    public void OnControlsButton()
    {

    }

    public void OnOptionsbutton()
    {

    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
