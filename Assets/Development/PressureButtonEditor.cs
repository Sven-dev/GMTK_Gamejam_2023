using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class PressureButtonEditor : MonoBehaviour
{
    [SerializeField] private PressureButton PressureButton;
    [Space]
    [SerializeField] private SpriteRenderer Platform;
    [SerializeField] private Transform UnpressedPivot;
    [SerializeField] private SpriteRenderer Background;

    [SerializeField] private BoxCollider2D PlatformCollider;

    private int Height = 0;
    private int PlatformSize = 0;

    private Vector3 Position;

    private void Update()
    {
        if (PressureButton.Height != Height)
        {
            Height = PressureButton.Height;
            Platform.transform.localPosition = new Vector2(0, Height);
            UnpressedPivot.localPosition = new Vector2(0, Height);
            Background.size = new Vector2(Background.size.x, Height);
        }

        if (PressureButton.PlatformSize != PlatformSize)
        {
            PlatformSize = PressureButton.PlatformSize;
            Platform.size = new Vector2(PlatformSize, Platform.size.y);
            PlatformCollider.size = new Vector2(PlatformSize, Platform.size.y);

            Position = Vector3.zero;
        }

        if (PressureButton.transform.position != Position)
        {
            float xpos = (int)PressureButton.transform.position.x;
            if (xpos > 0)
            {
                xpos -= 0.5f;
            }

            PressureButton.transform.position = new Vector3(
                    xpos,
                    (int)PressureButton.transform.position.y,
                    (int)PressureButton.transform.position.z);

            //Add an offset of half a tile if the length of the platform is uneven
            if (PlatformSize % 2 != 0)
            {
                PressureButton.transform.Translate(Vector3.right * -0.5f);
            }

            Position = PressureButton.transform.position;
        }
    }
}