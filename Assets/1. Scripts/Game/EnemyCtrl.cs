using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : EntityCtrl
{
    [Header("Settings")]
    [SerializeField] eEnemyType CurrentEnemyType;

    protected override void Start()
    {
        EntityProperties Properties = GameSettings.GetEnemyProperties(CurrentEnemyType);
        RefillStat(Properties);

        IsEnemy = true;
        IntegerForMove = -1;

        base.Start();

    }

}
