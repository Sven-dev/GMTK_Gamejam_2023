using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
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
    [SerializeField] private Animator Animator;

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

    public void UpdatePower(float power)
    {
        PowerLevel = power;
        if (power == 0)
        {
            Animator.SetBool("Moving", false);
        }
        else
        {
            Animator.SetBool("Moving", true);
        }
    }
}