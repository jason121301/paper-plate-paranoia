using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendScript : MonoBehaviour
{

    public bool isFollow = false;
    public int order = 0;
    private GameObject player;
    private float rotz;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (isFollow)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance > order * 1.5f)
            {
                Vector3 direction = (transform.position - player.transform.position).normalized;

                transform.position = player.transform.position + direction * order * 1.5f;
            }

            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            rotation(moveX, moveY);
        }
    }

    public void StartFollow(int i)
    {
        order = i;
        isFollow = true;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        Transform circleChild = transform.Find("Circle");
        if (circleChild != null)
        {
            Destroy(circleChild.gameObject);
        }

    }

    void rotation(float moveX, float moveY)
    {
        if (moveX > 0)
        {
            rotz += -Time.unscaledDeltaTime * rotationSpeed;
        }
        else if (moveX < 0)
        {
            rotz += Time.unscaledDeltaTime * rotationSpeed;
        }
        else if (moveY != 0)
        {
            rotz += -Time.unscaledDeltaTime * rotationSpeed;

        }




        transform.rotation = Quaternion.Euler(0, 0, rotz);
    }
}
