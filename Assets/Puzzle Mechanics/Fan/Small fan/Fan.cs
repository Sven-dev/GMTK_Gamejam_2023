using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Powerable
{
    [Header("Fan variables")]
    public Face Direction = Face.Right;
    [Range(1, 15)] public int Range = 3;
    [SerializeField] private float Strength = 1f;

    [Header("System variables")]
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Collider2D WindTrigger;
    [SerializeField] private Animator FanAnimator;
    [SerializeField] private SpriteRenderer WindAnimation;
    [SerializeField] private AudioSource Audio;

    private bool Powered = false;

    public override void UpdatePower(float power)
    {
        base.UpdatePower(power);
        if (AutoPower == false)
        {
            if (PowerLevel > 0)
            {
                if (!Powered)
                {
                    Powered = true;
                    Renderer.color = ColorDictionary.Instance.Powered;
                    FanAnimator.SetBool("Moving", true);
                    WindAnimation.enabled = true;
                    Audio.Play();
                }

            }
            else
            {
                if (Powered)
                {
                    Powered = false;
                    Renderer.color = ColorDictionary.Instance.Unpowered;
                    FanAnimator.SetBool("Moving", false);
                    WindAnimation.enabled = false;
                    Audio.Stop();
                }
            }
        }
        else //if (AutoPower == true)
        {
            if (PowerLevel > 0)
            {
                if (Powered)
                {
                    Powered = false;
                    Renderer.color = ColorDictionary.Instance.Unpowered;
                    FanAnimator.SetBool("Moving", false);
                    WindAnimation.enabled = false;
                    Audio.Stop();
                }
            }
            else
            {
                if (!Powered)
                {
                    Powered = true;
                    Renderer.color = ColorDictionary.Instance.Powered;
                    FanAnimator.SetBool("Moving", true);
                    WindAnimation.enabled = true;
                    Audio.Play();
                }
            }
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

            rigidbody.AddForce(dir * Strength * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}