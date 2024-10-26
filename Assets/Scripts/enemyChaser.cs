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

    private Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        timer = waitingTime;
        isWaiting = true;
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
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
                // reached waiting time
                timer = moveTime;   // reset
                playerPos = player.transform.position; // new direction to go
                isWaiting = false;  // can move
            }
            else
            {
                timer = waitingTime;
                isWaiting = true;
            }
        }
        if (!isWaiting)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        }
    }


    private void OnCollisionEnter2D(Collision2D collission)
    {
        if (collission.gameObject.name == "Player")
        {
            gameManager.EndGame();

        }
    }
        
}
