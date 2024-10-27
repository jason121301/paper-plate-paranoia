using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private float sizePerIncreaseX;
    private float sizePerIncreaseY;
    public float finalSizeX = 2f;
    public float finalSizeY = 0.5f;
    public float spawningDuration = 3f;
    private float currentSizeX = 0f;
    private float currentSizeY = 0f;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    public float lingeringDuration = 5f;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        sizePerIncreaseX = finalSizeX * 0.01f / spawningDuration;
        sizePerIncreaseY = finalSizeY * 0.01f / spawningDuration;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        GameObject manager = GameObject.Find("GameManager");
        gameManager = manager.GetComponent<GameManager>();

        StartCoroutine(Generating());
        StartCoroutine(DestroySelf());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Generating()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        while (currentSizeX <= finalSizeX)
        {
            yield return wait;
            currentSizeX += sizePerIncreaseX;
            currentSizeY += sizePerIncreaseY;
            gameObject.transform.localScale = new Vector3(currentSizeX, currentSizeY, currentSizeX);
            if (currentSizeX >= finalSizeX)
            {

                //renderer.color = Color.red;
                spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

    }

    private IEnumerator DestroySelf()
    {
        WaitForSeconds wait = new WaitForSeconds(lingeringDuration);
        yield return wait;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Wall"))
        {
            gameObject.transform.parent = collision.transform;
        }

        else if (collision.gameObject.name.Contains("Fork") || collision.gameObject.name.Contains("Spoon"))
        {
            Debug.Log("I have hit " + collision.gameObject.name);
            Vector3 originalScale = gameObject.transform.localScale;
            gameObject.transform.SetParent(collision.transform);
            gameObject.transform.localScale = originalScale;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
