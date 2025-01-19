using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : EntityCtrl
{
    [Header("Settings")]
    [SerializeField] eEnemyType CurrentEnemyType;

    public eEnemyType currentEnemyType => CurrentEnemyType;

    protected override void Start()
    {
        EntityProperties Properties = GameSettings.GetEnemyProperties(CurrentEnemyType);
        RefillStat(Properties);

        IsEnemy = true;
        IntegerForMove = -1;

        base.Start();

    }

    public override void OnDeadAnimationEvent()
    {
        LobbyMng.Instance.WhenEnemyDied(CurrentEnemyType, transform);
        base.OnDeadAnimationEvent();
    }

    protected void AttackSound()
    {
        string AudioName = "";
        if (CurrentEnemyType == eEnemyType.Sword)
        {
            AudioName = "SwordAttack";
        }
        else if (CurrentEnemyType == eEnemyType.Shield)
        {
            AudioName = "ShieldAttack";
        }
        else if (CurrentEnemyType == eEnemyType.Bow)
        {
            AudioName = "BowAttack";
        }
        AudioMng.Instance.PlayAudio(AudioName, 0.1f, false);
    }

    protected override void AttackAction()
    {
        base.AttackAction();
        AttackSound();
    }

}
