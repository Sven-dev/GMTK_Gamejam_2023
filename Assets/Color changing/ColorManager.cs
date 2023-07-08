using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Color LightColor;
    [SerializeField] private Color DarkColor;
    [Space]
    [SerializeField] private Color LightBackground;
    [SerializeField] private Color DarkBackground;
    [Space]
    [SerializeField] private Camera Camera;
    [Space]
    [SerializeField] private UnityColorEvent OnColorUpdate;


    // Start is called before the first frame update
    void Start()
    {
        OnColorUpdate.Invoke(DarkColor);
        Camera.backgroundColor = DarkBackground;
    }

    public void SwapColors(Sides side)
    {
        StartCoroutine(_SwapColors(side));        
    }

    private IEnumerator _SwapColors(Sides to)
    {
        Color foregroundFrom;
        Color foregroundTo;

        Color backgroundFrom;
        Color backgroundTo;

        if (to == Sides.Left)
        {
            foregroundFrom = LightColor;
            foregroundTo = DarkColor;

            backgroundFrom = LightBackground;
            backgroundTo = DarkBackground;
        }
        else // if (to == Sides.Right)
        {
            foregroundFrom = DarkColor;
            foregroundTo = LightColor;

            backgroundFrom = DarkBackground;
            backgroundTo = LightBackground;
        }

        float progress = 0;
        while (progress < 1)
        {
            progress += Mathf.Clamp01(Time.deltaTime);

            Camera.backgroundColor = Color.Lerp(backgroundFrom, backgroundTo, progress);
            OnColorUpdate?.Invoke(Color.Lerp(foregroundFrom, foregroundTo, progress));

            yield return null;
        }
    }
}