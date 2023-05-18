using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class replaybutton : MonoBehaviour
{
    public void LoadScene()
    {
        print("fuck you");
        SceneManager.LoadScene(0);
    }
}