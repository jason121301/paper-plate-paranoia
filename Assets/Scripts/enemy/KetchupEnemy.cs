using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

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

    private Animator anim;
    private bool flash;
    public GameObject laser;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        camera = Camera.main;
        rand_x = Random.Range(0, 2);
        rand_y = Random.Range(0, 2);
        //player = GameObject.Find("Player");

        waitTime = anim.runtimeAnimatorController.animationClips[0].length;
        flash = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (!flash)
        {
            Vector2 location = camera.ViewportToWorldPoint(new Vector3(rand_x, rand_y, camera.nearClipPlane));
            transform.position = location + new Vector2(-location.x, -location.y) * 0.15f;

            Vector2 direction = (transform.position - camera.transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }


        if (elapsedTime > waitTime)
        {
            Destroy(gameObject);
        }
    }

    public void OnAnimationFlash()
    {
        //Instantiate(laser, transform.position, Quaternion.identity);
        flash = true;
    }

}
