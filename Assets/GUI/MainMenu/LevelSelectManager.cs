using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Areas;
    [Space]
    [SerializeField] private List<UnityEngine.UI.Button> AreaButtons;

    public void LoadLevel(int levelIndex)
    {
        LevelManager.Instance.LoadLevel(levelIndex + 4, Transition.Crossfade);
        AudioManager.Instance.FadeOut("Main Menu", 2f);
    }

    public void SwitchArea(int areaIndex)
    {
        foreach (GameObject area in Areas)
        {
            area.SetActive(false);
        }

        Areas[areaIndex].SetActive(true);
    }

    public void SelectArea(int areaIndex)
    {
        AreaButtons[areaIndex].Select();
    }

    public void OnBackButton()
    {
        LevelManager.Instance.LoadLevel(1, Transition.Crossfade);
    }
}