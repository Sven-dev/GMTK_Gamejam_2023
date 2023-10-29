using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Transform GateTransform;
    [Space]
    [SerializeField] private Transform ActivePivot;
    [SerializeField] private Transform InactivePivot;

    [SerializeField] private bool On = false;

    private void Start()
    {
        if (On)
        {
            GateTransform.position = ActivePivot.position;
        }
        else
        {
            GateTransform.position = InactivePivot.position;
        }
    }

    public void Toggle(bool state)
    {
        On = !On;
        if (On)
        {
            StartCoroutine(_Move(InactivePivot.position, ActivePivot.position));
        }
        else
        {
            StartCoroutine(_Move(ActivePivot.position, InactivePivot.position));
        }
    }

    private IEnumerator _Move(Vector3 from, Vector3 to)
    {
        float progress = 0;
        while (progress < 1)
        {
            progress += Mathf.Clamp01(Time.deltaTime);

            GateTransform.position = Vector3.Lerp(from, to, progress);

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(ActivePivot.position, Vector3.one);
        Gizmos.DrawCube(InactivePivot.position, Vector3.one);
    }
}
