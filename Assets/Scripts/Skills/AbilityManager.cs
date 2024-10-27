using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class abilityManager : MonoBehaviour
{
    // attached to player
    private int numFork = 1;
    private int numUncontrollable = 1;
    private int numSpoon = 1;
    public Sprite surprisedFace;
    public GameObject fork;
    public GameObject spoon;
    //public GameObject cup;
    private GameObject playerObject;
    private playerMovement playerMovement;
    // Start is called before the first frame update

    public enum SkillType
    {
        None = 0,
        Flash = 1,
        Wipe = 2,
        Cup = 3,
        Fork = 4,
        Uncontrollable = 5
    }

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerMovement = playerObject.GetComponent<playerMovement>();
    }
    void Start()
    {
        // initialize all skills


        // game starts, have no ability
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (numUncontrollable >= 1)
            {
                GameObject.Find("UncontrollableAbilityCircle").GetComponent<Image>().color = Color.red;
                StartCoroutine(MoveUncontrollable());
                Debug.Log("Should move uncontrollably");
                numUncontrollable--;
                
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (numFork >= 1)
            {
                summonFork();
                Debug.Log("Should summon Fork");
                numFork--;
                GameObject.Find("ForkAbilityCircle").GetComponent<Image>().color = Color.red;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (numSpoon >= 1)
            {

                summonSpoon();
                numSpoon--;
                GameObject.Find("SpoonAbilityCircle").GetComponent<Image>().color = Color.red;
            }
        }
    }

    // script for fork
    void summonFork()
    {
        if (playerMovement.moveX != 0 || playerMovement.moveY != 0)
        {
            float angle = Mathf.Atan2(playerMovement.moveY, playerMovement.moveX) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 offset = rotation * Vector3.left * 20f;
            Vector3 spawnPosition = transform.position + offset;

            if (spawnPosition.x < -25)
            {
                spawnPosition.x -= (spawnPosition.x + 25) * 2;
            }
            else if (spawnPosition.x > 25)
            {
                spawnPosition.x -= (spawnPosition.x - 25) * 2;
            }
            if (spawnPosition.y < -25)
            {
                spawnPosition.y -= (spawnPosition.y + 25) * 2;
            }
            else if (spawnPosition.y > 25)
            {
                spawnPosition.y -= (spawnPosition.y - 25) * 2;
            }


                GameObject newFork = Instantiate(fork, spawnPosition, rotation);
        }
        else
        {
            // Instantiate fork with a default rotation if no input is detected
            GameObject newFork = Instantiate(fork, transform.position, Quaternion.identity);
        }

    }

    // script for cup
    void summonCup()
    {

    }

    void summonSpoon()
    {
        GameObject newSpoon = Instantiate(spoon, new Vector3(transform.position.x + 2f, transform.position.y, 0), Quaternion.identity);
        newSpoon.transform.parent = gameObject.transform;
        StartCoroutine(DestroyAfterDelay(newSpoon, 5f));
    }

    // script for flash
    void flash()
    {
        Quaternion direction = playerObject.transform.rotation;

        // displace the player object based on direction

    }

    // script for wipe
    void wipeFloor()
    {
        // find all enemies on scene and stop their movements

    }

    void uncontrollable()
    {

    }

    private IEnumerator MoveUncontrollable() 
    {
        playerMovement.collisionStatus = CollisionStatus.Kill;
        Sprite originalSprite = playerObject.GetComponent<SpriteRenderer>().sprite;
        playerObject.GetComponent<SpriteRenderer>().sprite = surprisedFace;
        float total_time = 2f;
        float current_time = 0f;
        while (total_time > current_time)
        {
            current_time += 0.3f;

            Vector2 shakePos = Random.insideUnitCircle * 20f;
            Vector3 finalDestination = new Vector3(shakePos.x, shakePos.y, 0);
            float time = Vector3.Distance(playerObject.transform.position, finalDestination) / (0.2f) * Time.deltaTime;
            while (transform.position.x != finalDestination.x)
            {
                transform.position = Vector3.MoveTowards(playerObject.transform.position, finalDestination, time);
                yield return null;
            }

        }
        playerMovement.collisionStatus = CollisionStatus.Vulnerable;
        playerObject.GetComponent<SpriteRenderer>().sprite = originalSprite;


    }

    private IEnumerator DestroyAfterDelay(GameObject item, float seconds)
    {
        WaitForSeconds wait = new WaitForSeconds(seconds);
        yield return wait;
        Destroy(item);
    }
}
