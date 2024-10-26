using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private float sizePerIncrease;
    public float finalSize = 1f;
    public float spawningDuration = 3f;
    private float currentSize = 0f;
    private SpriteRenderer renderer;
    private GameManager gameManager;

    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();

        sizePerIncrease = finalSize * 0.01f / spawningDuration;


        StartCoroutine(Generating());
    }

    private void OnCollisionEnter2D(Collision2D collission)
    {
        if (collission.gameObject.name == "Player" && renderer.color == Color.red)
        {
            //gameManager.EndGame();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Generating()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        while (currentSize <= finalSize)
        {
            yield return wait;
            currentSize += sizePerIncrease;
            gameObject.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
            if (currentSize >= finalSize)
            {

                renderer.color = Color.red;
            }
        }

    }
}
