using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : Powerable
{
    [Range(1, 10)] public int Length = 1;
    [SerializeField] [Range(25, 100)] [Tooltip("Note: player characters run at about 63 speed.")] 
    private float Speed = 1f;
    [SerializeField] private Direction Facing = Direction.Right;

    [SerializeField] private float PowerLevel = 0;
    private enum Direction
    {
        Left,
        Right,
    }

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Animator Animator;

    private void Start()
    {
        UpdatePower(0);
    }

    public override void UpdatePower(float power)
    {
        PowerLevel = power;
        if (power > 0)
        {
            Renderer.color = ColorDictionary.Instance.Powered;
            Animator.SetBool("Moving", true);
        }
        else
        {
            Renderer.color = ColorDictionary.Instance.Unpowered;
            Animator.SetBool("Moving", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PowerLevel > 0)
        {
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            if (Facing == Direction.Left)
            {
                rigidbody.AddForce(Vector2.left * Speed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                rigidbody.AddForce(Vector2.right * Speed * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
    }
}