using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(finalPosition.x, finalPosition.y), speed * Time.deltaTime);
        }
        if ((transform.position.x == finalPosition.x && transform.position.y == finalPosition.y) || triggerSpin)
        {
            animator.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = ratDead;
            currentRotation += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            StartCoroutine(DestroySelf());
        } 
    }

    private IEnumerator DestroySelf()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
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
        }
        else if (collission.gameObject.name.Contains("Fork")){
            isMoving = false;
            gameObject.transform.parent = collission.transform;
        }
    }
}
