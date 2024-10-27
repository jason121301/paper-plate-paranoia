using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{
    public float speed = 10f;
    private GameObject player;
    private GameManager gameManager;
    private playerMovement playerMovement;
    private Vector2 finalPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerMovement = player.GetComponent<playerMovement>();
        float final_x = 0;
        float final_y = 0;
        float diff_x = gameObject.transform.position.x - player.transform.position.x;
        float diff_y = gameObject.transform.position.y - player.transform.position.y;
        if (diff_x > 0)
        {
            final_x = gameObject.transform.position.x - diff_x * 2 + (playerMovement.moveX * playerMovement.moveSpeed);
        }
        else
        {
            final_x = gameObject.transform.position.x + diff_x * 2 + playerMovement.moveX * playerMovement.moveSpeed;
        }
        if (diff_y > 0)
        {
            final_y = gameObject.transform.position.y - diff_y * 2  + playerMovement.moveY * playerMovement.moveSpeed;
        }
        else
        {
            final_y = gameObject.transform.position.y + diff_y * 2 + playerMovement.moveY * playerMovement.moveSpeed;
        }

        transform.LookAt(new Vector2(final_x, final_y));
        finalPosition = new Vector2(final_x, final_y);
        var dir = new Vector3(finalPosition.x, finalPosition.y, 0f) - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(finalPosition.x, finalPosition.y), speed * Time.deltaTime);
        if (transform.position.x == finalPosition.x && transform.position.y == finalPosition.y)
        {
            StartCoroutine(DestroySelf());
        }
    }

    private IEnumerator DestroySelf()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collission)
    {
        if (collission.gameObject.name == "Player")
        {
            gameManager.EndGame();

        }
    }
}
