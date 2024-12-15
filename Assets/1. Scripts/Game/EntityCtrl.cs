using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCtrl : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;

    protected float speed;

    protected Transform CurrentTarget = null;

    protected bool CanMoving = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetBool("IsMoving", false);
        animator.SetBool("IsDead", false);

    }

    private void Start()
    {
        speed = Random.Range(.5f, 3f);
        CanMoving = true;

    }


    private void TryAttack()
    {
        animator.SetTrigger("DoAttack");
    }

    public void WhenCenserCollide(Transform TargetTransform, bool IsEnemy = false)
    {

        string TargetTag = "Enemy";

        if (IsEnemy)
        {
            TargetTag = "Ally";
        }

        Debug.Log(TargetTransform);

        if (TargetTransform.CompareTag(TargetTag))
        {
            CurrentTarget = TargetTransform;
            TryAttack();
            CanMoving = false;
        }


    }

    public void Attack()
    {
        Debug.Log("ATTACK");
    }


}
