using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClockController : MonoBehaviour
{
    [SerializeField] private List<Sprite> ClockSprites;
    [Space]
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private AudioSource TickSound;
    
    private float Time = 0;
    private float TickPitch = 1;

    public void SetClock(float time)
    {
        Time = Mathf.Clamp(time, 0, 8);
        Renderer.sprite = ClockSprites[(int)Time];
    }

    public void UpdateClock(float time)
    {
        Time = Mathf.Clamp(time, 0, 8);
        Renderer.sprite = ClockSprites[(int)Time];
        TickSound.pitch = Mathf.Lerp(0.25f, 1f, Time / 4);

        if (Time == 0)
        {
            TickSound.Stop();
        }
    }
}
