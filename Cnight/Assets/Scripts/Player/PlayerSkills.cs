using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour {

    public List<Skill> allSkills = new List<Skill>();
    public List<List<Skill>> combos = new List<List<Skill>>();
    public Skill counterAttack;

    private void Awake()
    {
        allSkills.Add((Skill) ScriptableObject.CreateInstance("OverheadAttack_2H"));
        allSkills.Add((Skill) ScriptableObject.CreateInstance("DeathA_2H"));

        combos.Add(new List<Skill> { allSkills[0] });
        combos.Add(new List<Skill> { allSkills[0], allSkills[1] });
        combos.Add(new List<Skill> { allSkills[0], allSkills[1], allSkills[0] });

        counterAttack = (Skill) ScriptableObject.CreateInstance("DeathC_2H");
    }
}
