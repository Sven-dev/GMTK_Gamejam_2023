using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBlock : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer.transform.Rotate(Vector3.forward * 100 * Time.deltaTime);
    }
}
