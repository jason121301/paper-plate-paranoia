using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{
    public float speed = 10f;
    private GameObject player;
    private GameManager gameManager;
    private playerMovement playerMovement;
    private Vector2 finalPosition;
    private float rotationSpeed =  1000f;
    private float currentRotation = 0f;
    public Sprite ratDead;
    private Animator animator;
    private bool triggerSpin =false;
    [System.NonSerialized] public bool isMoving = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerMovement = player.GetComponent<playerMovement>();
        animator = gameObject.GetComponent<Animator>();

        float final_x = 0;
        float final_y = 0;
        float diff_x = gameObject.transform.position.x - player.transform.position.x;
        float diff_y = gameObject.transform.position.y - player.transform.position.y;
        if (diff_x > 0)
        {
            final_x = (gameObject.transform.position.x - diff_x * 2 + (playerMovement.moveX * playerMovement.moveSpeed)) * 10;
        }
        else
        {
            final_x = (gameObject.transform.position.x +  diff_y * 2 + playerMovement.moveX * playerMovement.moveSpeed) * 10;
        }
        if (diff_y > 0)
        {
            final_y = (gameObject.transform.position.y - diff_y * 2  + playerMovement.moveY * playerMovement.moveSpeed) * 10;
        }
        else
        {
            final_y = (gameObject.transform.position.y + diff_y * 2 + playerMovement.moveY * playerMovement.moveSpeed) * 10;
        }

        transform.LookAt(new Vector2(final_x, final_y));
        finalPosition = new Vector2(final_x, final_y);
        var dir = new Vector3(finalPosition.x, finalPosition.y, 0f) - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        StartCoroutine(DestroySelf(20f));
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(finalPosition.x, finalPosition.y), speed * Time.deltaTime);
        }
        else
        {
            DestroySelf(3f);
        }
        if ((transform.position.x == finalPosition.x && transform.position.y == finalPosition.y) || triggerSpin)
        {
            animator.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = ratDead;
            currentRotation += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            StartCoroutine(DestroySelf(3f));
        } 
    }

    private IEnumerator DestroySelf(float seconds)
    {
        WaitForSeconds wait = new WaitForSeconds(seconds);
        yield return wait;
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collission)
    {
        if (collission.gameObject.name.Contains("Wall"))
        {
            animator.enabled = false;
            triggerSpin = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = ratDead;
            transform.SetParent(null, true);
        }
        else if (collission.gameObject.name.Contains("Fork") || collission.gameObject.name.Contains("Spoon"))
        {
                isMoving = false;
            if (collission.gameObject.name.Contains("Spoon"))
            {
                gameObject.transform.parent = collission.transform;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
