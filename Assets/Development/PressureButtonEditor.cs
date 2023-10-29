using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PressureButtonEditor : MonoBehaviour
{
    [SerializeField] private PressureButton PressureButton;
    [Space]
    [SerializeField] private Transform Platform;
    [SerializeField] private Transform UnpressedPivot;
    [SerializeField] private SpriteRenderer Background;

    private int Height = 0;

    private void Update()
    {
        if (PressureButton != null && PressureButton.Height != Height)
        {
            Height = PressureButton.Height;
            Platform.localPosition = new Vector2(0, Height);
            UnpressedPivot.localPosition = new Vector2(0, Height);
            Background.size = new Vector2(Background.size.x, Height);
        }
    }
}