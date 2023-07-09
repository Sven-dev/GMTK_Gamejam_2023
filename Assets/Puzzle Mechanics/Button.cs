using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Sprite UpSprite;
    [SerializeField] private Sprite DownSprite;
    [SerializeField] private SpriteRenderer Renderer;
    [Space]
    [SerializeField] private Collider2D Trigger;
    [SerializeField] private Collider2D Collider;
    [Space]
    [SerializeField] private UnityBoolEvent OnToggle;

    private int CycleCount = 0;
    private bool On = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Activate();
            AudioManager.Instance.Play("Button Press");
        }
    }

    private void Activate()
    {
        CycleCount = 2;
        On = true;      
        OnToggle?.Invoke(true);

        Renderer.sprite = DownSprite;
        Trigger.enabled = false;
        Collider.enabled = false;
    }
    private void Deactivate()
    {
        On = false;
        OnToggle?.Invoke(false);

        Renderer.sprite = UpSprite;
        Trigger.enabled = true;
        Collider.enabled = true;
    }

    public void OnRoleSwitch()
    {
        CycleCount--;
        if (CycleCount == 0)
        {
            Deactivate();
        }
    }
}

public interface IActivatable
{
     void Toggle(bool state);
}