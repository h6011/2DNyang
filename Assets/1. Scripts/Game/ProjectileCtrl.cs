using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCtrl : MonoBehaviour
{

    Rigidbody2D rb;

    private bool IsEnemyProjectile = false;
    private int IntegerForMove = 1;
    private float ProjectilePower = 5f;

    private string TargetTagName;

    bool IsDestroyed = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void SetIntegerForMove(bool BoolValue)
    {
        if (BoolValue)
        {
            IntegerForMove = -1;
        }
        else
        {
            IntegerForMove = 1;
        }
    }

    private void AutoSetTargetTagName()
    {
        if (IsEnemyProjectile)
        {
            TargetTagName = "Ally";
        }
        else
        {
            TargetTagName = "Enemy";
        }
    }

    public void SetIsEnemyProjectile(bool Value)
    {
        IsEnemyProjectile = Value;
        SetIntegerForMove(Value);
    }

    public void WhenOnSpawn()
    {
        rb.velocity = new Vector2(IntegerForMove * ProjectilePower, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        AutoSetTargetTagName();

        if (collision.CompareTag(TargetTagName))
        {
            if (!IsDestroyed)
            {
                EntityCtrl entityCtrl = collision.transform.parent.GetComponent<EntityCtrl>();
                if (entityCtrl != null)
                {
                    IsDestroyed = true;
                    entityCtrl.GetAttacked(1f);
                    DestroySelf();
                }
            }
        }

        

        
    }





}
