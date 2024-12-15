using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCtrl : EntityCtrl
{

    private void FixedUpdate()
    {
        if (CanMoving)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }


}
