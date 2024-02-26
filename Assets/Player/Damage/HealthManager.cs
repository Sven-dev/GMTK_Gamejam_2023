using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject Root;

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
        Death();
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
                    Death();
                }
            }
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
                    Death();
                }
            }
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
                        Death();
                        print("Crush");
                    }
                }
            }
        }
    }

    private void Death()
    {

        //Play death animation
        SpawnManager.Instance.Death();
        Destroy(Root);
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