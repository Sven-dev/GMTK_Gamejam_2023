using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDictionary : MonoBehaviour
{
    public static ColorDictionary Instance;

    public Color Background;
    public Color Unpowered;
    public Color Powered;
    public Color Highlight;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
