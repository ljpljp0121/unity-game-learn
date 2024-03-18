using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private bool canAttack;

    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOnDashOver;
    [SerializeField] private bool canCreateCloneOnCounterAttack;
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;

    [SerializeField] private bool crystalInsteadOfClone;
    public void CreateClone(Transform clonePosition, Vector3 offset)
    {
        if (SkillManager.instance.dash.cloneOnAttackUnlocked == true)
        {
            canDuplicateClone = true;
        }
        
        if(crystalInsteadOfClone)
        {
            SkillManager.instance.crystall.CreateCrystal();
            return;
        }
        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDuration, canAttack, offset,canDuplicateClone, chanceToDuplicate, player);
    }

    

    public void CreateCloneOnCounterAttack(Transform enemyTransform)
    {
        if(canCreateCloneOnCounterAttack)
        {
            StartCoroutine(CreateCloneWithDelay(enemyTransform,new Vector3(2*player.facingDir,0)));
        }
    }

    private IEnumerator CreateCloneWithDelay(Transform transform,Vector3 offset)
    {
        yield return new WaitForSeconds(.3f);
        CreateClone(transform,offset);
    }
}
