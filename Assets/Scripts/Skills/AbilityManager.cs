using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityManager : MonoBehaviour
{
    // attached to player
    public int numSkills = 5;
    public GameObject fork;
    public GameObject cup;
    private Skill[] skills;
    private int current;
    private GameObject playerObject;
    // Start is called before the first frame update

    public enum SkillType
    {
        None = 0,
        Flash = 1,
        Wipe = 2,
        Cup = 3,
        Fork = 4
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

        // game starts, have no ability
        current = 0;
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
}
