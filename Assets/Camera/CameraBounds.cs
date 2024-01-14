using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraBounds : MonoBehaviour
{
    [SerializeField] private Transform BoundPivotA;
    [SerializeField] private Transform BoundPivotB;

    public Vector3 RestrictCamera(Vector3 cameraPosition)
    {
        if (cameraPosition.x > BoundPivotB.position.x)
        {
            cameraPosition.x = BoundPivotB.position.x;
        }
        else if (cameraPosition.x < BoundPivotA.position.x)
        {
            cameraPosition.x = BoundPivotA.position.x;
        }

        if (cameraPosition.y < BoundPivotB.position.y)
        {
            cameraPosition.y = BoundPivotB.position.y;
        }
        else if (cameraPosition.y > BoundPivotA.position.y)
        {
            cameraPosition.y = BoundPivotA.position.y;
        }

        return cameraPosition;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 center = new Vector3(
            (BoundPivotA.position.x + BoundPivotB.position.x) /2,
            (BoundPivotA.position.y + BoundPivotB.position.y) /2,
            0);

        Vector3 size = new Vector3(
            BoundPivotB.position.x - BoundPivotA.position.x,
            BoundPivotB.position.y - BoundPivotA.position.y,
            0
            );

        if (size.x < 0 || size.y > 0)
        {
            Gizmos.color = Color.red;
        }
        else
        {
           Gizmos.DrawWireCube(center, size);
        }

        Gizmos.DrawCube(BoundPivotA.position, Vector3.one);
        Gizmos.DrawCube(BoundPivotB.position, Vector3.one);

        Gizmos.color = Color.cyan;

        size = new Vector3(
            (BoundPivotB.position.x - BoundPivotA.position.x) + Camera.main.orthographicSize * 3.55f,
            (BoundPivotB.position.y - BoundPivotA.position.y) - Camera.main.orthographicSize * 2,
            0
            );

        Gizmos.DrawWireCube(center, size);
    }
}