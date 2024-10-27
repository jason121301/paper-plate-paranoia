using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyMovement : MonoBehaviour
{
    private GameManager gameManager;
    public Animator animator;
    private GameObject player;

    [SerializeField] float speed = 5.0f;

    [SerializeField] float waitingTime = 0.5f;
    [SerializeField] float moveTime = 2.0f;
    private Vector3 targetPos;
    private float timer = 0f;
    private bool isWaiting;

    private int countDown;

    // Start is called before the first frame update
    void Start()
    {
        countDown = 3;
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
                countDown--;
                startShake(0.5f);
                //timer = waitingTime;
                isWaiting = true;
            }
        }
        if (isWaiting) 
        {
            if (countDown == 0)
            {
                startShake(0.5f);
                Debug.Log("destroy");
                Destroy(gameObject);
            }
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

    }
        
}
