using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCtrl : EntityCtrl
{
    [Header("Settings")]
    [SerializeField] eAllyType CurrentAllyType;

    protected override void Start()
    {
        EntityProperties Properties = GameSettings.GetAllyProperties(CurrentAllyType);
        RefillStat(Properties);

        IsEnemy = false;
        IntegerForMove = 1;

        base.Start();
    }

    



}
