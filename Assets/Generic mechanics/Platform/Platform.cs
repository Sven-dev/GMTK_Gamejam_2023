using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Collider2D Collider;

    private Input Input;

    private bool PlayerOnPlatform = false;

    // Start is called before the first frame update
    private void Start()
    {
        Input = new Input();
        Input.Enable();
    }

    // Update is called once per frame
    void Update()
    {

        float MovementInput = Input.Bigguy.Movement.ReadValue<Vector2>().y;
        print(Input.Bigguy.Movement.ReadValue<Vector2>());
        print(MovementInput);

        if (PlayerOnPlatform && MovementInput < 0)
        {
            Collider.enabled = false;
            Invoke("EnablePlatform", 1f);
            print("smaller than 0, collider disabled");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerOnPlatform = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerOnPlatform = false;
    }

    private void EnablePlatform()
    {
        Collider.enabled = true;
    }
}