using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private int id; // skill id
    private bool active; // if skill is the one being used

    public Skill(int id)
    {
        this.id = id;
        this.active = false;
    }

    public int Id { get => id; set => id = value; }
    public bool Active { get => active; set => active = value; }
}
