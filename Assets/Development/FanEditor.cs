using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FanEditor : MonoBehaviour
{
    [SerializeField] private Fan Fan;
    [Space]
    [SerializeField] private BoxCollider2D WindTrigger;
    [SerializeField] private SpriteRenderer WindAnimation;

    private Face Direction = Face.Right;
    private int Range = 0;

    private void Update()
    {
        if (Fan.Direction != Direction)
        {
            Vector3 rotation = Vector3.zero;
            switch(Fan.Direction)
            {
                case Face.Right:
                    rotation = Vector3.forward * 0;
                    break;
                case Face.Up:
                    rotation = Vector3.forward * 90;
                    break;
                case Face.Left:
                    rotation = Vector3.forward * 180;
                    break;
                case Face.Down:
                    rotation = Vector3.forward * 270;
                    break;
            }

            Fan.transform.eulerAngles = rotation;
            Direction = Fan.Direction;
        }

        if (Fan.Range != Range)
        {
            Range = Fan.Range;

            //On even ranges, there needs to be an offset of 0.5 more
            float offset = 0;
            if (Range %2 != 0)
            {
                offset = 0.5f;
            }

            WindTrigger.size = new Vector2(Range, 1);
            WindTrigger.offset = new Vector2(0.5f + Range / 2 + offset, 0);

            WindAnimation.size = new Vector2(Range, 1);
        }
    }
}