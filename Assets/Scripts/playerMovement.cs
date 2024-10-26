using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed; 

    private Rigidbody2D rb;
    public Vector2 movement;
    private float rotz;
    public float rotationSpeed;
    public bool rotationClock;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        processInputs(moveX,moveY);
        rotation(moveX, moveY);

    }
    void FixedUpdate()
    {
        move();
    }
    void processInputs(float moveX, float moveY)
    { 
        movement = new Vector2(moveX, moveY).normalized;
    }

    void getX()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
    }

    void rotation(float moveX, float moveY)
    {
        if (moveX > 0)
        {
            rotz += -Time.deltaTime * rotationSpeed;
        }
        else if(moveX < 0)
        {
            rotz += Time.deltaTime * rotationSpeed;
        }
        else if(moveY != 0)
        {
            rotz += -Time.deltaTime * rotationSpeed;

        }




        transform.rotation = Quaternion.Euler(0, 0, rotz);
    }


    void move()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }


    
}
