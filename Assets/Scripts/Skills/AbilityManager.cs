using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class abilityManager : MonoBehaviour
{
    // attached to player
    private int numF = 1;
    private int numQ = 1;
    private int numE = 1;
    public Sprite surprisedFace;
    public GameObject fork;
    public GameObject spoon;
    public GameObject clone;
    public List<SkillType> skillsToSprite;
    public List<Sprite> sprite;
    private GameObject playerObject;
    private playerMovement playerMovement;
    private List<SkillType> currentSkills = new List<SkillType>();
    
    // Start is called before the first frame update

    public enum SkillType
    {
        Clone,
        Spoon,
        Fork,
        Uncontrollable
    }

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerMovement = playerObject.GetComponent<playerMovement>();
    }
    void Start()
    {
        // initialize random skills
        List<SkillType> skillsList = new List<SkillType>();
        foreach (SkillType skill in Enum.GetValues(typeof(SkillType)))
        {
            skillsList.Add(skill);
        }
        while (currentSkills.Count < 3)
        {
            int i = UnityEngine.Random.Range(0, skillsList.Count);
            currentSkills.Add(skillsList[i]);
            skillsList.RemoveAt(i);
        }
        bool qDone = false;
        bool eDone = false;
        foreach (SkillType item in currentSkills)
        {
            int index = skillsToSprite.FindIndex(skill => skill == item);
            if (!qDone)
            {
                GameObject.Find("QImage").GetComponent<Image>().sprite = sprite[index];
                qDone = true;
            }
            else if (!eDone)
            {
                GameObject.Find("EImage").GetComponent<Image>().sprite = sprite[index];
                eDone = true;
            }
            else
            {
                GameObject.Find("FImage").GetComponent<Image>().sprite = sprite[index];
            }
        }
    }

    void ActivateSkill(SkillType skill)
    {
        if (skill == SkillType.Clone)
        {
            summonClone();
        }
        else if (skill == SkillType.Spoon)
        {
            summonSpoon();
        }
        else if (skill == SkillType.Fork)
        {
            summonFork();
        }
        else if (skill == SkillType.Uncontrollable) 
        {
            StartCoroutine(MoveUncontrollable());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (numQ >= 1)
            {
                GameObject.Find("QAbility").GetComponent<Image>().color = Color.red;
                ActivateSkill(currentSkills[0]);
                numQ--;
                
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (numF >= 1)
            {
                ActivateSkill(currentSkills[1]);
                numF--;
                GameObject.Find("EAbility").GetComponent<Image>().color = Color.red;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (numE >= 1)
            {
                ActivateSkill(currentSkills[2]);
                numE--;
                GameObject.Find("FAbility").GetComponent<Image>().color = Color.red;
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
            Vector3 spawnPosition = playerObject.transform.position + offset;

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

    void summonClone()
    {
        for (int x = -4; x <= 4; x = x + 4)
        {
            for (int y = -4; y <= 4; y = y + 4)
            {
                if (!(y == 0 && x == 0)) { 
                GameObject newClone = Instantiate(clone, new Vector3(playerObject.transform.position.x + x, playerObject.transform.position.y + y, 0), Quaternion.identity);

                //newClone.transform.SetParent(gameObject.transform);
            }
            }
        }
    }

    // script for cup
    void summonCup()
    {

    }

    void summonSpoon()
    {
        GameObject newSpoon = Instantiate(spoon, new Vector3(playerObject.transform.position.x + 2f, playerObject.transform.position.y, 0), Quaternion.identity);
        newSpoon.transform.parent = gameObject.transform;
        StartCoroutine(DestroyAfterDelay(newSpoon, 10f));
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

            Vector2 shakePos = UnityEngine.Random.insideUnitCircle * 20f;
            Vector3 finalDestination = new Vector3(shakePos.x, shakePos.y, 0);
            float time = Vector3.Distance(playerObject.transform.position, finalDestination) / (0.2f) * Time.deltaTime;
            while (playerObject.transform.position.x != finalDestination.x)
            {
                playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, finalDestination, time);
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
