using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyMovement : MonoBehaviour
{
    private GameManager gameManager;
    public Animator animator;
    private GameObject player;
    private CircleCollider2D collider;

    [SerializeField] float speed = 5.0f;

    [SerializeField] float waitingTime = 0.5f;
    [SerializeField] float moveTime = 2.0f;
    private Vector3 targetPos;
    private float timer = 0f;
    private bool isWaiting;
    public bool isMoving = true;
    public int countDown;

    // Start is called before the first frame update
    void Start()
    {
        countDown = 3;
        timer = waitingTime;
        isWaiting = true;
        isMoving = true;
        player = GameObject.Find("Player");
        GameObject manager = GameObject.Find("GameManager");
        gameManager = manager.GetComponent<GameManager>();
        collider = gameObject.GetComponent<CircleCollider2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("countDown", countDown);
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
                startShake(0.5f);
                collider.radius += 0.05f;
                countDown--;
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
            if (isMoving)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
        }

    }

    public void startShake(float duration)
    {
        StartCoroutine(shake(duration));
    }
    public IEnumerator shake(float duration)
    {
        Vector2 originalPos = transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            Vector2 shakePos = Random.insideUnitCircle * 0.1f;
            transform.position = originalPos + shakePos;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;

        if (countDown == 0)
        {
            collider.radius = 3.0f;
            if ((player.transform.position - transform.position).sqrMagnitude < collider.radius * collider.radius)
            {
                gameManager.EndGame();
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Fork"))
        {
            isMoving = false;
            gameObject.transform.parent = collision.transform;
        }
    }

}
