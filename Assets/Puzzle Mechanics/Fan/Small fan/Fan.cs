using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Powerable
{
    public Face Direction = Face.Right;
    [Range(1, 10)] public int Range = 3;

    [SerializeField] private float Strength = 1f;
    [Space]
    [SerializeField] private Collider2D WindTrigger;
    [SerializeField] private Animator Animator;
    [SerializeField] private SpriteRenderer WindAnimation;

    private float PowerLevel = 0;

    private void Start()
    {
        UpdatePower(0);
    }

    public override void UpdatePower(float power)
    {
        PowerLevel = power;
        if (power > 0)
        {
            Animator.SetBool("Moving", true);
            WindAnimation.enabled = true;
        }
        else
        {
            Animator.SetBool("Moving", false);
            WindAnimation.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PowerLevel > 0 && collision.tag == "Player")
        {
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();

            Vector2 dir = Vector2.zero;
            switch (Direction)
            {
                case Face.Right:
                    dir = Vector2.right;
                    break;
                case Face.Up:
                    dir = Vector2.up;
                    break;
                case Face.Left:
                    dir = Vector2.left;
                    break;
                case Face.Down:
                    dir = Vector2.down;
                    break;
            }

            rigidbody.AddForce(dir * Strength * PowerLevel * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}

public enum Face
{
    Up,
    Down,
    Left,
    Right
}