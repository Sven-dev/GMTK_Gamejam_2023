using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private string Name;

    public void Trigger()
    {
        AudioManager.Instance.Play(Name);
    }
}
