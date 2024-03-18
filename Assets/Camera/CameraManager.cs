using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Range(1, 20f)]
    [SerializeField] private float MovementSmoothing = 0.5f;
    [SerializeField] private CameraBounds Bounds;
    [SerializeField] private Transform Target;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (Target == null)
        {
            if (SpawnManager.Instance.Body != null)
            {
                Target = SpawnManager.Instance.Body.transform;
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }
        }
        else //if (Target != null)
        {
            FollowCharacter();
        }
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

        transform.position = Bounds.RestrictCamera(transform.position);
    }
}