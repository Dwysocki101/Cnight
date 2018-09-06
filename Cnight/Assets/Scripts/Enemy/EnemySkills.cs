using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkills : MonoBehaviour {

    public List<Skill> allSkills = new List<Skill>();
    public List<List<Skill>> combos = new List<List<Skill>>();

    private void Awake()
    {
        allSkills.Add((Skill) ScriptableObject.CreateInstance("MagicAttack02"));
        allSkills.Add((Skill) ScriptableObject.CreateInstance("MagicAttack03"));

        combos.Add(new List<Skill> { allSkills[0] });
        combos.Add(new List<Skill> { allSkills[1], allSkills[0] });
        combos.Add(new List<Skill> { allSkills[1], allSkills[0], allSkills[1] });
    }
}
