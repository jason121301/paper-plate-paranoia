using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloneScript : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    public Vector2 movement;
    private float rotz;
    public float rotationSpeed;
    public bool rotationClock;
    private GameManager gameManager;

    [System.NonSerialized] public float moveX;
    [System.NonSerialized] public float moveY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(DestroySelf(5f));
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        processInputs(moveX, moveY);
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
        else if (moveX < 0)
        {
            rotz += Time.deltaTime * rotationSpeed;
        }
        else if (moveY != 0)
        {
            rotz += -Time.deltaTime * rotationSpeed;

        }




        transform.rotation = Quaternion.Euler(0, 0, rotz);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            if (collision.otherCollider.gameObject == this.gameObject)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DestroySelf(float seconds)
    {
        WaitForSeconds wait = new WaitForSeconds(seconds);
        yield return wait;
        Destroy(gameObject);
    }

    void move()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }
}
