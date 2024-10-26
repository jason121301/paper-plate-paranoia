using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] float initialRate = 5f;
    [SerializeField] float rateIncrease = 1.01f;
    [SerializeField] float rangeX = 5;
    [SerializeField] float rangeY = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(initialRate);

        while (true)
        {
            yield return wait;
            float rand_x = Random.Range(gameObject.transform.position.x - rangeX, gameObject.transform.position.x + rangeX);
            float rand_y = Random.Range(gameObject.transform.position.y - rangeY, gameObject.transform.position.y + rangeY);
            GameObject newEnemy = Instantiate(enemyToSpawn, new Vector2(rand_x, rand_y), Quaternion.identity);
            newEnemy.transform.parent = gameObject.transform;
            initialRate = initialRate / rateIncrease;
            
        }
    }
}
