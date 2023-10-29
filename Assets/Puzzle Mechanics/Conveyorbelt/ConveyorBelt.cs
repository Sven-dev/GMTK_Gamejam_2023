using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private bool Left;
    [SerializeField] private float Speed = 1f;
    [Space]
    [SerializeField] private Animator Animator;

    [SerializeField] private bool On = false;

    private void Start()
    {
        Animator.SetBool("Moving", On);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (On && collision.tag == "Player")
        {
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            if (Left)
            {
                rigidbody.AddForce(Vector2.left * Speed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                rigidbody.AddForce(Vector2.right * Speed * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
    }

    public void Toggle(bool state)
    {
        On = !On;
        Animator.SetBool("Moving", On);
    }
}