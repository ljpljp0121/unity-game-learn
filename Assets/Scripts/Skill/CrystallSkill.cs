using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallSkill : Skill
{
    [SerializeField] private float crystallDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystall;

    public override void UseSkill()
    {
        base.UseSkill();

        if(currentCrystall == null )
        {
            currentCrystall = Instantiate(crystalPrefab,player.transform.position,Quaternion.identity);
            CrystallSkillController crystallSkillController = currentCrystall.GetComponent<CrystallSkillController>();

            crystallSkillController.SetupCrystall(crystallDuration);
        }
        else
        {
            player.transform.position = currentCrystall.transform.position;
            Destroy(currentCrystall);
        }
    }
}
