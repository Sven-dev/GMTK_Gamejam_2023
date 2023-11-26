using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firejet : Powerable
{

    [Range(1, 10)]
    public int Range = 3;
    [Space]
    [SerializeField] private SpriteRenderer BlockRenderer;
    
    [Header("Jet")]
    [SerializeField] private SpriteRenderer JetRenderer;
    [SerializeField] private BoxCollider2D JetTrigger;
    [Space]
    [SerializeField] private LayerMask DetectionMask;
    [SerializeField] private Transform RaycastPivot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (PowerLevel > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(RaycastPivot.position, transform.right, Range, 1 << LayerMask.NameToLayer("Ground"));
            print(hit.distance);
            if (hit.collider != null)
            {
                JetTrigger.size = new Vector2(hit.distance, JetTrigger.size.y);
                JetTrigger.offset = new Vector2(hit.distance/2, JetTrigger.offset.y);

                JetRenderer.size = new Vector2(hit.distance, 1);
            }
            else
            {
                JetTrigger.size = new Vector2(Range, JetTrigger.size.y);
                JetTrigger.offset = new Vector2(Range / 2, JetTrigger.offset.y);

                JetRenderer.size = new Vector2(Range, 1);
            }
        }
        else
        {
            JetTrigger.size = new Vector2(Range, JetTrigger.size.y);
            JetTrigger.offset = new Vector2(Range / 2, JetTrigger.offset.y);

            JetRenderer.size = new Vector2(Range, 1);
        }
    }

    public override void UpdatePower(float power)
    {
        base.UpdatePower(power);
        if (PowerLevel > 0)
        {
            JetRenderer.gameObject.SetActive(true);
            BlockRenderer.color = ColorDictionary.Instance.Powered;
        }
        else
        {
            JetRenderer.gameObject.SetActive(false);
            BlockRenderer.color = ColorDictionary.Instance.Unpowered;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(RaycastPivot.position, transform.right * Range);
    }
}
