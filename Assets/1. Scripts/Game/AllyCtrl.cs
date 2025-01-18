using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCtrl : EntityCtrl
{
    [Header("Settings")]
    [SerializeField] eAllyType CurrentAllyType;

    public eAllyType currentAllyType => CurrentAllyType;

    protected override void Start()
    {
        EntityProperties Properties = GameSettings.GetAllyProperties(CurrentAllyType);
        RefillStat(Properties);

        IsEnemy = false;
        IntegerForMove = 1;

        base.Start();
    }

    protected void AttackSound()
    {
        string AudioName = "";
        if (CurrentAllyType == eAllyType.Sword)
        {
            AudioName = "SwordAttack";
        }
        else if (CurrentAllyType == eAllyType.Shield)
        {
            AudioName = "ShieldAttack";
        }
        else if (CurrentAllyType == eAllyType.Bow)
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
