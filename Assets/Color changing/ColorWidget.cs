using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ColorWidget : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Image Image;
    [SerializeField] private Tilemap Tilemap;

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
    }
}