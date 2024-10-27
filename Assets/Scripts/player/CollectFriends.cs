using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFriends : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] GameObject friend1;
    [SerializeField] GameObject friend2;
    [SerializeField] GameObject friend3;
    private List<GameObject> friends;

    private Camera camera;
    private int collected;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        collected = 0;
        friends.Add(friend1);
        friends.Add(friend2);
        friends.Add(friend3);

        spawnFriend(collected);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Friend") && collision.otherCollider.gameObject == this.gameObject)
        {
            collected++;
            if (collected == 3)
            {
                gameManager.WinGame();
            }
            spawnFriend(collected);
        }
    }

    private void spawnFriend(int friendInt)
    {
        camera = Camera.main;
        int rand_x = Random.Range(0, 2);
        int rand_y = Random.Range(0, 2);
        Vector2 location = camera.ViewportToWorldPoint(new Vector3(rand_x, rand_y, camera.nearClipPlane));
        location += new Vector2(location.x, location.y) * 3f;
        location = new Vector2(Mathf.Clamp(location.x, -20, 20), Mathf.Clamp(location.y, -20, 20));

        Instantiate(friends[friendInt], location, Quaternion.Euler(Vector3.forward));
    }

}
