using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement: MonoBehaviour
{
    public Rigidbody2D character;
    public int movementspeed;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.D))
        {
            
            character.position += Vector2.right * Time.deltaTime * movementspeed;
            sprite.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            character.position += Vector2.left * Time.deltaTime * movementspeed;
            sprite.flipX = true;

        }
        if (Input.GetKey(KeyCode.W))
        {
            character.position += Vector2.up * Time.deltaTime * movementspeed;

        }
        if (Input.GetKey(KeyCode.S))
            character.position += Vector2.down * Time.deltaTime * movementspeed;
    }
}
