using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, IActivatable
{
    [SerializeField] private float Speed = 1f;
    [Space]
    [SerializeField] private Transform UpPivot;
    [SerializeField] private Transform DownPivot;
    [SerializeField] private Transform ElevatorTransform;

    private bool On = false;

    private IEnumerator ElevatorCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        ElevatorCoroutine = _MoveElevator();
        if (On)
        {
            StartCoroutine(ElevatorCoroutine);
        }
    }

    public void Toggle(bool value)
    {
        On = !On;
        if (On)
        {
            StartCoroutine(ElevatorCoroutine);
        }
        else
        {
            StopCoroutine(ElevatorCoroutine);
        }
    }

    private IEnumerator _MoveElevator()
    {
        Transform from = DownPivot;
        Transform to = UpPivot;
        while (true)
        {
            while (ElevatorTransform.position != UpPivot.position)
            {
                ElevatorTransform.position = Vector2.MoveTowards(from.position, to.position, Speed);

                yield return null;
            }

            Transform temp = from;
            from = to;
            to = temp;

            yield return new WaitForSeconds(2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(UpPivot.position, Vector3.one);
        Gizmos.DrawCube(DownPivot.position, Vector3.one);
    }
}
