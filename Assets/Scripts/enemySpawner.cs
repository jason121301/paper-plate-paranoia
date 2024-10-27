using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] float initialRate = 5f;
    [SerializeField] float rateIncrease = 1.01f;
    [SerializeField] float rangeX = 5;
    [SerializeField] float rangeY = 5;
    [SerializeField] float timeBeforeSpawn = 0f;
    [SerializeField] EnemyType enemy;
    private GameObject cameraObject;
    private Camera mainCamera;
    private int amount_miss = 0;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = GameObject.Find("Main Camera");

        if (cameraObject != null)
        {
            mainCamera = cameraObject.GetComponent<Camera>();
        }
        StartCoroutine(Spawner());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds initialWait = new WaitForSeconds(timeBeforeSpawn);
        yield return initialWait;

        WaitForSeconds wait = new WaitForSeconds(initialRate);

        while (true)
        {
            yield return wait;

            Vector2 location = getLocation();
            while (Mathf.Abs(location.x) > 25 || Mathf.Abs(location.y) > 25)
            {
                location = getLocation();
                amount_miss += 1;
                Debug.Log(amount_miss + " total items have been spawned outside the wall");
                yield return null;
            }       
            GameObject newEnemy = Instantiate(enemyToSpawn, location, Quaternion.identity);
            newEnemy.transform.parent = gameObject.transform;
            initialRate = initialRate / rateIncrease;   
            
        }
    }

    private Vector2 getLocation()
    {
        float rand_x = Random.Range(-rangeX, rangeX);
        float rand_y = Random.Range(-rangeY, rangeY);
        Vector2 location = new Vector2(rand_x, rand_y);
        if (enemy == EnemyType.Chaser)
        {
            float rand_variable = Random.Range(0f, 1f);
            float rand = Random.Range(0f, 1f);
            if (rand_variable < 0.25)
            {
                location = mainCamera.ViewportToWorldPoint(new Vector3(0f, rand, mainCamera.nearClipPlane));
            }
            else if (0.25 <= rand_variable && rand_variable < 0.5)
            {
                location = mainCamera.ViewportToWorldPoint(new Vector3(1f, rand, mainCamera.nearClipPlane));
            }
            else if (0.5 <= rand_variable && rand_variable < 0.75)
            {
                location = mainCamera.ViewportToWorldPoint(new Vector3(rand, 0f, mainCamera.nearClipPlane));
            }
            else if (0.75 <= rand_variable)
            {
                location = mainCamera.ViewportToWorldPoint(new Vector3(rand, 1f, mainCamera.nearClipPlane));
            }

        }

        if (enemy == EnemyType.Shadow)
        {
            Vector3 middle = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, mainCamera.nearClipPlane));
            location = new Vector2(middle.x + rand_x, middle.y + rand_y);
        }

        if (enemy == EnemyType.Rat)
        {
            float rand_variable = Random.Range(0f, 1f);
            float rand = Random.Range(0f, 1f);
            if (rand_variable < 0.25)
            {
                location = mainCamera.ViewportToWorldPoint(new Vector3(0f, rand, mainCamera.nearClipPlane));
                location.x -= Random.Range(1f, 3f);
            }
            else if (0.25 <= rand_variable && rand_variable < 0.5)
            {
                location = mainCamera.ViewportToWorldPoint(new Vector3(1f, rand, mainCamera.nearClipPlane));
                location.x += Random.Range(1f, 3f);
            }
            else if (0.5 <= rand_variable && rand_variable < 0.75)
            {
                location = mainCamera.ViewportToWorldPoint(new Vector3(rand, 0f, mainCamera.nearClipPlane));
                location.y -= Random.Range(1f, 3f);
            }
            else if (0.75 <= rand_variable)
            {
                location = mainCamera.ViewportToWorldPoint(new Vector3(rand, 1f, mainCamera.nearClipPlane));
                location.y += Random.Range(1f, 3f);
            }
        }
        return location;
    }
}


public enum EnemyType
{
    Chaser,
    Shadow,
    Rat
}
