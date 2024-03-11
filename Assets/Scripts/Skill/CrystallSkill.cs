using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallSkill : Skill
{
    [SerializeField] private float crystallDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystall;
    [Header("Crystal mirage")]
    [SerializeField] private bool cloneInsteadOfCrystal;
    public override void UseSkill()
    {
        base.UseSkill();

        if (currentCrystall == null)
        {
            CreateCrystal();
        }
        else
        {

            if (cloneInsteadOfCrystal)
                SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
            Destroy(currentCrystall);
        }
        player.transform.position = currentCrystall.transform.position;
        Destroy(currentCrystall);
    }

    public void CreateCrystal()
    {
        currentCrystall = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        CrystallSkillController crystallSkillController = currentCrystall.GetComponent<CrystallSkillController>();

        crystallSkillController.SetupCrystall(crystallDuration);
    }
}

