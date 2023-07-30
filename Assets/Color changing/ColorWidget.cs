using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class ColorWidget : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Image Image;
    [SerializeField] private Tilemap Tilemap;
    [SerializeField] private TextMeshPro Text;

    private void Awake()
    {
        ColorManager.OnColorUpdate.AddListener(UpdateColor);
    }

    public void UpdateColor(Color color)
    {
        if (SpriteRenderer != null)
        {
            SpriteRenderer.color = color;
        }

        if (Image != null)
        {
            Image.color = color;
        }

        if (Tilemap != null)
        {
            Tilemap.color = color;
        }

        if (Text != null)
        {
            Text.color = color;
        }
    }
}