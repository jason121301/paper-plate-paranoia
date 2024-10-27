using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupEnemy : MonoBehaviour
{
    // get camera
    // position follow camera position until idk how many seconds
    private Camera camera;
    //private GameObject player;
    private float elapsedTime;
    [SerializeField] float waitTime;

    private float rand_x;
    private float rand_y;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rand_x = Random.Range(0, 1);
        rand_y = Random.Range(0, 1);
        //player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 location = camera.ViewportToWorldPoint(new Vector3(rand_x, rand_y, camera.nearClipPlane));

        transform.position = location;
    }
}
