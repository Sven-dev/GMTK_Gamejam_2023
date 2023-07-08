using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGUI : MonoBehaviour
{
    [SerializeField] private Image TimerLight;
    [SerializeField] private Image TimerDark;

    public void SwitchRole(Sides side)
    {

    }

    public void UpdateTime(float timePassed)
    {
        //Timer.time
    }
}
