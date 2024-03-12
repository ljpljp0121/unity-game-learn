using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [Header("Major Stats")]
    public Stat strength;//damage
    public Stat agility;//evasion
    public Stat intelligence;//magic,每1减3
    public Stat vitality;//health

    [Header("Offensive Stats")]
    public Stat damage;
    //伤害增幅概率
    public Stat critChance;
    //伤害倍率
    public Stat critPower;

    [Header("Defensive Stats")]
    public Stat maxHp;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Magic Stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;

    public bool isIgnited; //每秒造成伤害
    public bool isChilled; // 降低20%护甲
    public bool isShocked; // %20无法命中概率

    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;



    private float igniteDamageCooldown = .3f;
    private float igniteDamageTimer;
    private int igniteDamage;
    public int currentHp;

    public System.Action onHealthChanged;

    private void Awake()
    {
        currentHp = GetMaxHealthValue();
    }
    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
    }

    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;
        igniteDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0)
        {
            isIgnited = false;
        }
        if (chilledTimer < 0)
        {
            isChilled = false;
        }
        if (shockedTimer < 0)
        {
            isShocked = false;
        }

        if (igniteDamageTimer <= 0 && isIgnited)
        {
            Debug.Log("每次受到" + igniteDamage + "点燃烧伤害");
            DecreaseHealthBy(igniteDamage);
            if (currentHp < 0)
            {
                Die();
            }
            igniteDamageTimer = igniteDamageCooldown;
        }

    }
    //造成伤害
    public virtual void DoDamage(CharacterStats targetStats)
    {
        if (TargetCanAvoidAttack(targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();
        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }
        totalDamage = CheckTargetArmor(targetStats, totalDamage);
        targetStats.TakeDamage(totalDamage);
        DoMagicalDamage(targetStats);
    }
    //造成魔法伤害
    public virtual void DoMagicalDamage(CharacterStats characterStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        int totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();
        totalMagicalDamage = CheckTargetResistance(characterStats, totalMagicalDamage);

        characterStats.TakeDamage(totalMagicalDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
        {
            return;
        }

        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _lightingDamage && _iceDamage > _fireDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        while (!canApplyChill && !canApplyIgnite && !canApplyShock)
        {
            if (Random.value < .33f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                characterStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("fireDamage");
                return;
            }

            if (Random.value < .33f && _iceDamage > 0)
            {
                canApplyChill = true;
                characterStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("iceDamage");
                return;
            }

            if (Random.value < .33f && _lightingDamage > 0)
            {
                canApplyShock = true;
                characterStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("lightDamage");
                return;
            }
        }
        if (canApplyIgnite)
        {
            characterStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));
        }

        characterStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);



    }
    //魔法伤害计算
    private static int CheckTargetResistance(CharacterStats characterStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= characterStats.magicResistance.GetValue() + (characterStats.intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }
    //异常效果判定
    public void ApplyAilments(bool ignite, bool chill, bool shock)
    {
        if (isIgnited || isChilled || isShocked)
        {
            return;
        }
        if (ignite)
        {
            isIgnited = ignite;
            ignitedTimer = 2;
        }
        if (chill)
        {
            isChilled = chill;
            chilledTimer = 2;
        }
        if (shock)
        {
            isShocked = shock;
            shockedTimer = 2;
        }
    }
    //火焰燃烧异常伤害
    public void SetupIgniteDamage(int damage) => igniteDamage = damage;

    //生命值减少与死亡判定    
    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;
        DecreaseHealthBy(damage);
        if (currentHp < 0)
        {
            Die();
        }

    }
    //生命条减少
    protected virtual void DecreaseHealthBy(int damage)
    {
        currentHp -= damage;
        if (onHealthChanged != null)
        {
            onHealthChanged();
        }
    }
    //死亡
    protected virtual void Die()
    {

    }
    //物理伤害计算
    private int CheckTargetArmor(CharacterStats targetStats, int totalDamage)
    {
        if (isChilled)
        {
            totalDamage -= Mathf.RoundToInt(targetStats.armor.GetValue() * .8f);
        }
        else
        {
            totalDamage -= targetStats.armor.GetValue();
        }
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }
    //闪避判断
    private bool TargetCanAvoidAttack(CharacterStats targetStats)
    {
        int totalEvasion = targetStats.evasion.GetValue() + targetStats.agility.GetValue();

        if (isShocked)
            totalEvasion += 20;

        if (Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("Attack Avoided");
            return true;
        }
        return false;
    }
    //伤害增幅判定
    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();
        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    //伤害增幅计算
    private int CalculateCriticalDamage(int damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;
        float critDamage = damage * totalCritPower;
        return Mathf.RoundToInt(critDamage);
    }
    //最大生命值
    public int GetMaxHealthValue()
    {
        return maxHp.GetValue() + vitality.GetValue() * 5;
    }
}
