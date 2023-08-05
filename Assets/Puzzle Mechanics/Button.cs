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

    private string ButtonPress;

    private void Start()
    {
        Label.text = ActiveTime.ToString();
        ButtonPress = AudioManager.Instance.CreateSound("Button Press");
    }

    private void OnDestroy()
    {
        AudioManager.Instance.DestroySound(ButtonPress);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Activate();
            AudioManager.Instance.SetPitch(ButtonPress, 1f);
            AudioManager.Instance.Play(ButtonPress);
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
        int time = ActiveTime;
        while (time > -1)
        {
            yield return new WaitForSeconds(1);

            time -= 1;
            Label.text = time.ToString();        
        }

        Label.text = ActiveTime.ToString();

        //Check if the player is still standing on the button
        if (Physics2D.OverlapBox(transform.position + Vector3.up * 0.35f, Vector3.right * 0.8f + Vector3.up * 0.6f, 0, LayerMask.GetMask("Player")))
        {
            StartCoroutine(_Timer());
        }
        else
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        On = false;
        OnToggle?.Invoke(false);

        Renderer.sprite = UpSprite;
        Trigger.enabled = true;
        Collider.enabled = true;

        AudioManager.Instance.SetPitch(ButtonPress, 0.7f);
        AudioManager.Instance.Play(ButtonPress);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + Vector3.up * 0.35f, Vector3.right * 0.8f + Vector3.up * 0.6f);
    }
}

public interface IActivatable
{
     void Toggle(bool state);
}