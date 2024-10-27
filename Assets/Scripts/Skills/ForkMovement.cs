using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class ForkMovment : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 finalPosition;
    private float speed = 9f;
    private bool hitWall = false;
    void Start()
    {
        Vector3 facingDirection = transform.right;

        // Set the distance the fork should travel
        float distance = 100f;
        finalPosition = new Vector2(
            transform.position.x + facingDirection.x * distance,
            transform.position.y + facingDirection.y * distance
        );

    }

    // Update is called once per frame
    void Update()
    {
        if (!hitWall) { 
            transform.position = Vector2.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Wall"))
        {
            hitWall = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DestroySelf());
        }
    }

    private IEnumerator DestroySelf()
    {
        WaitForSeconds wait = new WaitForSeconds(5f);
        yield return wait;
        Destroy(gameObject);
    }   
}
