using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicController : MonoBehaviour
{
    [SerializeField] private string LeftMusic;
    [SerializeField] private string RightMusic;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.FadeIn(LeftMusic, 0.5f);
    }

    public void OnSwitch(bool player1)
    {
        if (player1)
        {
            AudioManager.Instance.FadeOut(RightMusic, 1f, false);
            AudioManager.Instance.FadeIn(LeftMusic, 1f);
        }
        else
        {
            AudioManager.Instance.FadeOut(LeftMusic, 1f, false);
            AudioManager.Instance.FadeIn(RightMusic, 1f);
        }     
    }

    public void OnVictory()
    {
        AudioManager.Instance.FadeOut(RightMusic, 1f);
        AudioManager.Instance.FadeOut(LeftMusic, 1f);

        AudioManager.Instance.Play("Kiss");
    }
}