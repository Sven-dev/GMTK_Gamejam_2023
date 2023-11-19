using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderApplier : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private Shader Shader;

    private void Start()
    {
        test();
    }

    private void test()
    {
        Camera.RenderWithShader(Shader, "");
    }
}