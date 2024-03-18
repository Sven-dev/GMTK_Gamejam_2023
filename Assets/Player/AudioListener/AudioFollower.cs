using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFollower : MonoBehaviour
{
    public static AudioFollower Instance;

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
            }
        }
        else //if (Target != null)
        {
            FollowTarget();
        }
    }

    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
    }

    private void FollowTarget()
    {
        Vector3 characterPosition = Target.position;
        characterPosition.z = transform.position.z;

        if (transform.position != characterPosition)
        {
            transform.position += (characterPosition - transform.position);
        }
    }
}
