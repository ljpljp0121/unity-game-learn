using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    public PlayerManager playerManager;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    public override void DoDamage(CharacterStats targetStats)
    {
        base.DoDamage(targetStats);
        playerManager.souls += totalDamage;
    }

    protected override void Die()
    {
        base.Die();
        player.Die();

        GetComponent<PlayerItemDrop>().GenerateDrop();
    }
}
