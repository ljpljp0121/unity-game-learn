using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public DashSkill dash {  get; private set; }
    public CloneSkill clone { get; private set; }
    public SwordSkill sword { get; private set; }

    public BlackHoleSkill blackHole { get; private set; }
    public CrystallSkill crystall { get; private set; }
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
        sword = GetComponent<SwordSkill>();
        blackHole = GetComponent<BlackHoleSkill>();
        crystall = GetComponent<CrystallSkill>();
    }
}
