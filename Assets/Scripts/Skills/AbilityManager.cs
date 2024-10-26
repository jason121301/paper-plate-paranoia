using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityManager : MonoBehaviour
{
    // attached to player
    public int numSkills = 5;
    //public GameObject fork;
    //public GameObject cup;
    private Skill[] skills;
    private int current;
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
    void Start()
    {
        // initialize all skills
        skills = new Skill[numSkills];
        addSkill(SkillType.None);
        addSkill(SkillType.Flash);
        addSkill(SkillType.Wipe);
        addSkill(SkillType.Cup);
        addSkill(SkillType.Fork);

        playerObject = GameObject.Find("Player");
        playerMovement = playerObject.GetComponent<playerMovement>();

        // game starts, have no ability
        current = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(moveUncontrollable());
            Debug.Log("Should move uncontrollably");
        }
    }

    void addSkill(SkillType t)
    {
        skills[(int)t] = new Skill((int)t);
    }
    public void acquireSkill(SkillType t)
    {
        skills[(int)t].Active = true;
    }

    void activateSkill(SkillType t)
    {
        // call the function related to that skill

        // turn off the skill
        skills[(int)t].Active = false;
    }

    // script for fork
    void summonFork()
    {
        // make the fork appear
    }

    // script for cup
    void summonCup()
    {

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

    private IEnumerator moveUncontrollable() 
    {
        playerMovement.collisionStatus = CollisionStatus.Kill;
        float total_time = 2f;
        float current_time = 0f;
        bool x_direction = true;
        bool y_direction = true;
        int i = 0;
        float new_x = 0;
        float new_y = 0;
        while (total_time > current_time)
        {
            current_time += 0.3f;
            i++;
            //if (x_direction == true)
            //{
            //    new_x = transform.position.x + Random.Range(5f, 7f);
            //    x_direction = false;
            //}
            //else
            //{
            //    new_x = transform.position.x - Random.Range(5f, 7f);
            //    x_direction = true;
            //}
            //if (y_direction == true)
            //{
            //    new_y = transform.position.y + Random.Range(5f, 7f);
            //    y_direction = false;
            //}
            //else
            //{
            //    new_y = transform.position.y - Random.Range(5f, 7f);
            //    y_direction = true;
            //}

            //Vector3 finalDestination = new Vector3(new_x, new_y, 0);

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
    }
}
