using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    [Tooltip("Should the button power the room on or off?")]
    public bool AutoPower = true;
    [Space]
    [SerializeField] private BoxCollider2D Trigger;
    [SerializeField] private Animator Animator;
    [SerializeField] private AudioSource ClickSound;

    private Room Room;

    private void Start()
    {
        Room = transform.GetComponentInParent<Room>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Turn on button
        Press();

        if (AutoPower == true)
        {
            Room.TurnOn();
        }
        else
        {
            Room.TurnOff();
        }
    }

    /// <summary>
    /// Sets the state of the button with causing any effects
    /// </summary>
    public void SetState(bool value)
    {
        if (AutoPower)
        {
            if (value == true)
            {
                Trigger.enabled = false;
                Animator.Play("On");

            }
            else
            {
                Trigger.enabled = true;
                Animator.Play("Off");
            }
        }
        else
        {
            if (value == true)
            {
                Trigger.enabled = true;
                Animator.Play("Off");
            }
            else
            {
                Trigger.enabled = false;
                Animator.Play("On");
            }
        }
    }

    public void Press()
    {
        Trigger.enabled = false;
        Animator.Play("On");
        ClickSound.Play();
    }

    public void Depress()
    {
        Trigger.enabled = true;
        Animator.Play("Off");
    }
}