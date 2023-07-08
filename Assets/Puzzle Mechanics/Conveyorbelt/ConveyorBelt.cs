using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour, IActivatable
{
    [SerializeField] private Sides Direction = Sides.Right;
    [SerializeField] private float Speed = 1f;
    [Space]
    [SerializeField] private Animator Animator;

    private bool On = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (On && collision.tag == "Player")
        {
            print("In trigger");
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            if (Direction == Sides.Right)
            {
                rigidbody.AddForce(Vector2.right * Speed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else //if (Direction == Sides.Left)
            {
                rigidbody.AddForce(Vector2.left * Speed * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
    }

    public void Toggle(bool state)
    {
        On = !On;
        Animator.SetBool("Moving", On);
    }
}