using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFan : MonoBehaviour
{
    [SerializeField] private int Direction = 1;
    [SerializeField] private float Speed = 1f;
    [Space]
    [SerializeField] private Collider2D WindTrigger;
    [SerializeField] private Animator Animator;

    [SerializeField] private bool On = false;

    private void Start()
    {
        WindTrigger.enabled = On;
        Animator.SetBool("Moving", On);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (On && collision.tag == "Player")
        {
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            if (Direction == 1)
            {
                rigidbody.AddForce(Vector2.up * Speed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else //if (Direction == -1)
            {
                rigidbody.AddForce(Vector2.down * Speed * Time.deltaTime, ForceMode2D.Impulse);
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
