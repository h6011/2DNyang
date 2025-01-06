using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : EntityCtrl
{

    protected override void Start()
    {
        base.Start();

        IsEnemy = true;
        integer = -1;

    }

}
