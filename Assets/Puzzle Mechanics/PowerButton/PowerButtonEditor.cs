using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PowerButtonEditor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Animator Animator;
    [SerializeField] private Collider2D Trigger;
    [Space]
    [SerializeField] private PowerButton PowerButton;

    private bool AutoPower = false;
    private Room Room;

    // Start is called before the first frame update
    private void Awake()
    {
        Room = transform.GetComponentInParent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PowerButton.AutoPower != AutoPower)
        {
            AutoPower = PowerButton.AutoPower;

            if (AutoPower == true)
            {
                Renderer.color = ColorDictionary.Instance.Powered;
                if (Room.Powered)
                {
                    Animator.Play("On");
                    Trigger.enabled = false;
                }
                
            }
            else
            {
                Renderer.color = ColorDictionary.Instance.Unpowered;
                if (Room.Powered)
                {
                    Animator.Play("Off");
                    Trigger.enabled = true;
                }
            }
        }
    }
}
