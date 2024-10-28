using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupEnemy : MonoBehaviour
{
    private Collider2D collider;
    private Rigidbody2D rb;

    private float elapsedTime;
    private float waitTime;

    private Camera camera;
    private float rand_x;
    private float rand_y;

    private Animator anim;
    private bool flash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        collider.enabled = false;

        anim = GetComponent<Animator>();
        camera = Camera.main;
        rand_x = Random.Range(0, 2);
        rand_y = Random.Range(0, 2);

        waitTime = anim.runtimeAnimatorController.animationClips[0].length;
        flash = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (!flash)
        {
            Vector2 location = 0.85f * camera.ViewportToWorldPoint(new Vector3(rand_x, rand_y, camera.nearClipPlane));
            Vector2 cameraPos = new Vector2(camera.transform.position.x, camera.transform.position.y);
            Vector2 direction = (location - cameraPos).normalized;
            float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            transform.position = location - 13.7f * direction;
        }


        if (elapsedTime > waitTime)
        {
            Destroy(gameObject);
        }
    }

    public void OnAnimationFlash()
    {
        flash = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void OnAnimationSqueeze()
    {
        collider.enabled = true;
        Debug.Log(collider.enabled);
    }

}
