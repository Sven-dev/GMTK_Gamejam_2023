using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConveyorBeltEditor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private BoxCollider2D BaseCollider;
    [SerializeField] private BoxCollider2D MovementTrigger;
    [SerializeField] private AudioSource Audio;
    [Space]
    [SerializeField] private ConveyorBelt ConveyorBelt;

    private int Length = 1;
    private Vector3 Position;

    // Update is called once per frame
    private void Update()
    {
        if (ConveyorBelt != null && ConveyorBelt.Length != Length)
        {
            Length = ConveyorBelt.Length;
            SpriteRenderer.size = new Vector2(Length, SpriteRenderer.size.y);

            BaseCollider.size = new Vector2(Length, BaseCollider.size.y);
            BaseCollider.offset = new Vector2(Length/2f, BaseCollider.offset.y);

            MovementTrigger.size = new Vector2(Length, MovementTrigger.size.y);
            MovementTrigger.offset = new Vector2(Length/2f, MovementTrigger.offset.y);

            Audio.transform.localPosition = new Vector2(Length / 2f, 0.35f);
            Audio.minDistance = Length / 2f;
            Audio.maxDistance = Length / 2f + 1;
        }
    }
}