using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenserCtrl : MonoBehaviour
{
    EntityCtrl entityCtrl;

    private void Awake()
    {
        entityCtrl = transform.parent.GetComponent<EntityCtrl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        entityCtrl.WhenCenserCollide(collision.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        entityCtrl.WhenCenserCollide(collision.transform);
    }

}
