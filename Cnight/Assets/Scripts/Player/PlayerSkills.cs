using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour {

    public List<Skill> allSkills = new List<Skill>();
    public List<List<Skill>> combos = new List<List<Skill>>();

    private void Awake()
    {
        allSkills.Add(new OverheadAttack_2H());
        allSkills.Add(new DeathA_2H());

        combos.Add(new List<Skill> { allSkills[0] });
        combos.Add(new List<Skill> { allSkills[0], allSkills[1] });
        combos.Add(new List<Skill> { allSkills[0], allSkills[1], allSkills[0] });
    }
}
