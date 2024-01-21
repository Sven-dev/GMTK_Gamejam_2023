using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClockController : MonoBehaviour
{
    [SerializeField] private List<Sprite> ClockSprites;
    [Space]
    [SerializeField] private SpriteRenderer Renderer;

    public void UpdateClock(float time)
    {
        time = Mathf.Clamp(time, 0, 8);
        Renderer.sprite = ClockSprites[(int)time];
    }
}
