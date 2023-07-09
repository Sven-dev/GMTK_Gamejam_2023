using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorSwitcher : MonoBehaviour
{
    [SerializeField] private Color ColorA;
    [SerializeField] private Color ColorB;
    [Space]
    [SerializeField] private Image ImageLeft;
    [SerializeField] private Image ImageRight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Switch());
    }

    IEnumerator _Switch()
    {
        while (true)
        {
            yield return new WaitForSeconds(8);

            float progress = 0;
            while (progress < 1)
            {
                progress += Mathf.Clamp01(Time.deltaTime/2);

                ImageLeft.color = Color.Lerp(ColorA, ColorB, progress);
                ImageRight.color = Color.Lerp(ColorB, ColorA, progress);


                yield return null;
            }

            Color temp = ColorA;
            ColorA = ColorB;
            ColorB = temp;
        }
    }
}
