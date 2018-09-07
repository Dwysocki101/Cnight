using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {
    [SerializeField]
    public new string name;
    [SerializeField]
    public string animationName;

    public int damage = 0;
}
