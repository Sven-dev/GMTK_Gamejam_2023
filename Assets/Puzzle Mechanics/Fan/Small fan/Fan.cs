using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour, IActivatable
{
    [SerializeField] private bool Left;
    [SerializeField] private float Speed = 1f;
    [Space]
    [SerializeField] private Collider2D WindTrigger;
    [SerializeField] private Animator Animator;

    private bool On = false;

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

        WindTrigger.enabled = On;
    }
}
