using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firejet : Powerable
{
    [Range(2, 10)]
    public int Range = 2;
    public Face Direction;
    [Space]
    [SerializeField] private SpriteRenderer BlockRenderer;
    
    [Header("Jet")]
    [SerializeField] private BoxCollider2D JetTrigger;
    [SerializeField] private SpriteRenderer JetRenderer;
    [SerializeField] private Animator JetAnimator;  
    [Space]
    [SerializeField] private LayerMask DetectionMask;
    [SerializeField] private Transform RaycastPivot;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (JetRenderer.gameObject.activeInHierarchy)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(RaycastPivot.position, transform.right, Range, 1 << LayerMask.NameToLayer("Ground"));

            //get the first detected obstacle that isn't the firejet itself
            //This could be improved
            RaycastHit2D hit = new RaycastHit2D();
            foreach (RaycastHit2D h in hits)
            {
                if (!h.transform.name.Contains("Firejet"))
                {
                    hit = h;
                }
            }

            //If theres more than 1 collider in front of the jet (collider 0 is the firejet block)
            if (hit.collider != null)
            {
                JetTrigger.size = new Vector2(hit.distance, JetTrigger.size.y);
                JetTrigger.offset = new Vector2(hit.distance / 2, JetTrigger.offset.y);

                JetRenderer.size = new Vector2(hit.distance, 1);

                JetAnimator.Play("Blocked");
            }
            else
            {
                JetTrigger.size = new Vector2(Range, JetTrigger.size.y);
                JetTrigger.offset = new Vector2(Range / 2, JetTrigger.offset.y);

                JetRenderer.size = new Vector2(Range, 1);

                JetAnimator.Play("On");
            }
        }
    }

    public override void UpdatePower(float power)
    {
        base.UpdatePower(power);
        if (AutoPower == false)
        {
            if (PowerLevel > 0)
            {
                JetRenderer.gameObject.SetActive(true);
                //BlockRenderer.color = ColorDictionary.Instance.Powered;
            }
            else
            {
                JetRenderer.gameObject.SetActive(false);
                //BlockRenderer.color = ColorDictionary.Instance.Unpowered;
            }
        }
        else if (AutoPower == true)
        {
            if (PowerLevel > 0)
            {
                JetRenderer.gameObject.SetActive(false);
                //BlockRenderer.color = ColorDictionary.Instance.Unpowered;
            }
            else
            {
                JetRenderer.gameObject.SetActive(true);
                //BlockRenderer.color = ColorDictionary.Instance.Powered;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(RaycastPivot.position, transform.right * Range);
    }
}
