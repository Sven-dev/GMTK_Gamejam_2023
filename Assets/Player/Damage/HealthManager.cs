using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Crush checking")]
    [SerializeField] private LayerMask Mask;
    [Space]
    [SerializeField] private Transform UpPivot;
    [SerializeField] private Transform DownPivot;
    [Space]
    [SerializeField] private Transform UpLeftPivot;
    [SerializeField] private Transform UpRightPivot;
    [Space]
    [SerializeField] private Transform DownLeftPivot;
    [SerializeField] private Transform DownRightPivot;

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.zero;
        CrushCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnManager.Instance.Death();
        Destroy(gameObject);
    }

    private void CrushCheck()
    {
        RaycastHit2D rayA;
        RaycastHit2D rayB;

        //Up-down detection
        rayA = Physics2D.BoxCast(UpPivot.position, new Vector2(0.5f, 0.01f), 0, Vector2.up, 0.5f, Mask.value);
        rayB = Physics2D.BoxCast(DownPivot.position, new Vector2(0.5f, 0.01f), 0, Vector2.down, 0.5f, Mask.value);
        if (rayA.transform != null)
        {
            if (rayB.transform != null)
            {
                if (rayA.distance == 0 && rayB.distance == 0)
                {
                    print("Crush");
                    SpawnManager.Instance.Death();
                    Destroy(gameObject);
                }
            }
        }

        if (rayA.transform != null)
        {
            print("Ray up: " + rayA.transform.name + ", Distance: " + rayA.distance);
        }

        if (rayB.transform != null)
        {
            print("Ray down: " + rayB.transform.name + ", Distance: " + rayB.distance);
        }

        //upleft-upright detection 1
        rayA = Physics2D.BoxCast(UpLeftPivot.position, new Vector2(0.5f, 0.01f), 0, Vector2.up, 0.5f, Mask.value);
        rayB = Physics2D.BoxCast(UpRightPivot.position, new Vector2(0.5f, 0.01f), 0, Vector2.down, 0.5f, Mask.value);
        if (rayA.transform != null)
        {         
            if (rayB.transform != null)
            {
                if (rayA.distance == 0 && rayB.distance == 0)
                {
                    print("Crush");
                    SpawnManager.Instance.Death();
                    Destroy(gameObject);
                }
            }
        }

        if (rayA.transform != null)
        {
            print("Ray upleft: " + rayA.transform.name + ", Distance: " + rayA.distance);
        }

        if (rayB.transform != null)
        {
            print("Ray upright: " + rayB.transform.name + ", Distance: " + rayB.distance);
        }

        if (DownLeftPivot != null && DownRightPivot != null)
        {
            //downleft-downright detection
            rayA = Physics2D.BoxCast(DownLeftPivot.position, new Vector2(0.5f, 0.01f), 0, Vector2.up, 0.5f, Mask.value);
            rayB = Physics2D.BoxCast(DownRightPivot.position, new Vector2(0.5f, 0.01f), 0, Vector2.down, 0.5f, Mask.value);
            if (rayA.transform != null)
            {
                if (rayB.transform != null)
                {
                    if (rayA.distance == 0 && rayB.distance == 0)
                    {
                        print("Crush");
                        SpawnManager.Instance.Death();
                        Destroy(gameObject);
                    }
                }
            }

            if (rayA.transform != null)
            {
                print("Ray downleft: " + rayA.transform.name + ", Distance: " + rayA.distance);
            }

            if (rayB.transform != null)
            {
                print("Ray downright: " + rayB.transform.name + ", Distance: " + rayB.distance);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(UpPivot.position + Vector3.up * 0.25f, new Vector2(0.5f, 0.01f) + Vector2.up * 0.5f);
        Gizmos.DrawWireCube(DownPivot.position + Vector3.down * 0.25f, new Vector2(0.5f, 0.01f) + Vector2.down * 0.5f);

        Gizmos.DrawWireCube(UpLeftPivot.position + Vector3.left * 0.25f, new Vector2(0.01f, 0.5f) + Vector2.right * 0.5f);
        Gizmos.DrawWireCube(UpRightPivot.position + Vector3.right * 0.25f, new Vector2(0.01f, 0.5f) + Vector2.right * 0.5f);
        if (DownLeftPivot != null && DownRightPivot != null)
        {
            Gizmos.DrawWireCube(DownLeftPivot.position + Vector3.left * 0.25f, new Vector2(0.01f, 0.5f) + Vector2.right * 0.5f);
            Gizmos.DrawWireCube(DownRightPivot.position + Vector3.right * 0.25f, new Vector2(0.01f, 0.5f) + Vector2.right * 0.5f);
        }
    }
}