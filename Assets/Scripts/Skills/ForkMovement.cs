using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ForkMovment : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 finalPosition;
    private float speed = 10f;
    void Start()
    {
        finalPosition = new Vector2(transform.position.x + 100f, transform.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);
    }
}
