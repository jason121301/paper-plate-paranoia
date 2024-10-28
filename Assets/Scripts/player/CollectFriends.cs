using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFriends : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] GameObject friend1;
    [SerializeField] GameObject friend2;
    [SerializeField] GameObject friend3;
    [SerializeField] GameObject friend4;
    private List<GameObject> friends;
    private bool canCollect = true;
    private Vector2 friendLocation;
    public GameObject friendPointer;
    public Sprite happySprite;

    private Camera camera;
    private int collected;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        collected = 0;
        friends = new List<GameObject>();
        friends.Add(friend1);
        friends.Add(friend2);
        friends.Add(friend3);
        friends.Add(friend4);

        spawnFriend(collected);
        
    }

    // Update is called once per frame
    void Update()
    {
        friendPointer.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2f, 0);
        friendPointer.transform.LookAt(friendLocation);
        var dir = new Vector3(friendLocation.x, friendLocation.y, 0f) - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        friendPointer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Friend") && collision.otherCollider.gameObject == this.gameObject)
        {
            
            
            if (canCollect)
            {
                StartCoroutine(smileForDuration(2f));
                collected++;
                Debug.Log("I have colledted " + collected + " friends");
                canCollect = false;
                StartCoroutine(collectDelay());
                if (collected == 4)
                {
                    gameManager.WinGame();
                }
                else
                {
                    spawnFriend(collected);
                }
                Destroy(collision.gameObject);
            }
            

        }
    }

    private IEnumerator collectDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        yield return wait;
        canCollect = true;
    }

    private void spawnFriend(int friendInt)
    {
        camera = Camera.main;

        Vector2 location = new Vector2(0,0);
        while (Vector2.Distance(gameObject.transform.position, location) < 20f)
        {
            float rand_x = Random.Range(-2f, 3f);
            Debug.Log(rand_x + " is rand_x");
            float rand_y = Random.Range(-2f, 3f);
            Debug.Log(rand_y + " is rand_y");
            location = new Vector2(rand_x, rand_y) * 60f;
            location = new Vector2(Mathf.Clamp(location.x, -23f, 23f), Mathf.Clamp(location.y, -23f, 23f));
        }
        Instantiate(friends[friendInt], location, Quaternion.Euler(Vector3.forward));
        friendLocation = location;
    }

    private IEnumerator smileForDuration (float seconds)
    {

        Sprite originalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = happySprite;
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<SpriteRenderer>().sprite = originalSprite;
    }

}
