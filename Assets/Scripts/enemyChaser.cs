using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] float speed = 5.0f;

    [SerializeField] float waitingTime = 1.0f;
    [SerializeField] float moveTime = 5.0f;
    private float timer = 0f;
    private bool isWaiting;
    private GameManager gameManager;

    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        timer = waitingTime;
        isWaiting = true;
        player = GameObject.Find("Player");
        GameObject manager = GameObject.Find("GameManager");
        gameManager = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (isWaiting)
            {
                timer = moveTime;
                isWaiting = false;
            }
            else
            {
                timer = waitingTime;
                isWaiting = true;
            }
        }
        if (isWaiting) 
        {
            targetPos = player.transform.position;
            Vector2 direction = (transform.position - targetPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gameManager.EndGame();

        }
    }
        
}
