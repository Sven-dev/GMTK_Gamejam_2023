using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private string Name;
    [SerializeField] private bool PlayOnStart = false;

    private void Start()
    {
        if (PlayOnStart)
        {
            Trigger();
        }
    }

    public void Trigger()
    {
        AudioManager.Instance.Play(Name);
    }
}
