using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkill : Skill
{
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private float maxSize;
    [SerializeField]  private float growSpeed;
    [SerializeField] private float shrinkSpeed;
    [Space]
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float cloneAttackCooldown;

    BlackHoleSkillController currentBlackHole;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackHole = Instantiate(blackHolePrefab,player.transform.position,Quaternion.identity);

        currentBlackHole = newBlackHole.GetComponent<BlackHoleSkillController>();

        currentBlackHole.SetupBlackHole(maxSize,growSpeed,shrinkSpeed,amountOfAttacks,cloneAttackCooldown);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool BlackHoleFinished()
    {
        if(!currentBlackHole)
            return false;

        if (currentBlackHole.playerCanExitState)
        {
            currentBlackHole = null;
            return true;
        }

        return false;
    }
}
