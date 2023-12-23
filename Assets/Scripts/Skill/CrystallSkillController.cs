using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallSkillController : MonoBehaviour
{
    private float crystalDuration = Mathf.Infinity;
    public void SetupCrystall(float crystalDuration)
    {
        this.crystalDuration = crystalDuration;
    }

    private void Update()
    {
        crystalDuration -= Time.deltaTime;
        if (crystalDuration < 0)
        {
            SelfDestroy();
        }
    }

    public void SelfDestroy() => Destroy(gameObject);
}
