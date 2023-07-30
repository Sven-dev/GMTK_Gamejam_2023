using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    [SerializeField] private int ActiveTime = 5;
    [Space]
    [SerializeField] private Sprite UpSprite;
    [SerializeField] private Sprite DownSprite;
    [SerializeField] private SpriteRenderer Renderer;
    [Space]
    [SerializeField] private Collider2D Trigger;
    [SerializeField] private Collider2D Collider;
    [SerializeField] private TextMeshPro Label;
    [Space]
    [SerializeField] private UnityBoolEvent OnToggle;

    private int CycleCount = 0;
    private bool On = false;

    private void Start()
    {
        Label.text = ActiveTime.ToString();
    }

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

        StartCoroutine(_Timer());
    }

    private IEnumerator _Timer()
    {
        yield return new WaitForSeconds(1);

        int time = ActiveTime;
        while (time > 0)
        {    
            time -= 1;
            Label.text = time.ToString();

            yield return new WaitForSeconds(1);
        }

        Label.text = ActiveTime.ToString();
        Deactivate();
    }

    private void Deactivate()
    {
        On = false;
        OnToggle?.Invoke(false);

        Renderer.sprite = UpSprite;
        Trigger.enabled = true;
        Collider.enabled = true;
    }
}

public interface IActivatable
{
     void Toggle(bool state);
}