using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Range(1, 20f)]
    [SerializeField] private float MovementSmoothing = 0.5f;
    [SerializeField] private Transform Target;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        FollowCharacter();
    }

    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
    }

    private void FollowCharacter()
    {
        Vector3 characterPosition = Target.position;
        characterPosition.z = transform.position.z;

        if (transform.position != characterPosition)
        {
            transform.position += (characterPosition - transform.position) / MovementSmoothing;
        }
    }
}