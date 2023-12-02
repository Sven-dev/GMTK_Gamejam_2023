using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FirejetEditor : MonoBehaviour
{
    [SerializeField] private Firejet Firejet;
    [Space]
    [SerializeField] private BoxCollider2D FireTrigger;
    [SerializeField] private SpriteRenderer FireRenderer;

    private Face Direction;
    private int Range = 0;

    // Update is called once per frame
    private void Update()
    {
        if (Firejet.Direction != Direction)
        {
            Vector3 rotation = Vector3.zero;
            switch (Firejet.Direction)
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

            transform.eulerAngles = rotation;
            Direction = Firejet.Direction;
        }

        if (Firejet.Range != Range)
        {
            Range = Firejet.Range;

            //On even ranges, there needs to be an offset of 0.5 more
            float offset = 0;
            if (Range % 2 != 0)
            {
                offset = 0.5f;
            }

            FireTrigger.size = new Vector2(Range, 1);
            FireTrigger.offset = new Vector2(Range / 2 + offset, 0);

            FireRenderer.size = new Vector2(Range, 1);
        }
    }
}