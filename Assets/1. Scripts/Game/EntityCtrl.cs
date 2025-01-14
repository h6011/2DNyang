using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class EntityCtrl : MonoBehaviour
{
    [Header("Mng")]
    protected PrefabMng prefabMng;

    [Header("Objects [READ ONLY]")]
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Transform HitboxTrans;

    // ATTACK TYPE LIKE BOW

    protected int IntegerForMove = 1;

    [Header("Stat")]
    [SerializeField] protected float MaxHp;
    [SerializeField] protected float Hp;
    [SerializeField] protected float Speed;
    [SerializeField] protected float DetectDistance;
    [SerializeField] protected float Damage;
    [SerializeField] protected float AttackCooldown;

    [SerializeField] protected eEntityAttackType EntityAttackType;
    [SerializeField] protected string ProjectilePrefabName;
    [SerializeField] protected GameObject ProjectilePrefabObject;


    [Header("ToCheck [READ ONLY]")]
    [SerializeField] protected bool IsDead = false;
    [SerializeField] protected bool IsEnemy = false;
    [SerializeField] protected bool IsKnockbacked = false;
    [SerializeField] protected bool IsGroundedForDisplay = false;

    [SerializeField] protected bool CanMoving = false;


    [Header("Tick [READ ONLY]")]
    [SerializeField] protected float AttackTick = 0f;


    protected float KnockbackForceFieldTick = 0f;
    protected float KnockbackForceFieldTime = 0.1f;


    private void OnValidate()
    {
        
    }


    protected virtual void Awake()
    {
        InitalLoadingAction();
        InitalAnimatorAction();
        prefabMng = PrefabMng.Instance;
    }

    protected virtual void Start()
    {
        CanMoving = true;
        RefilHp();
    }

    /// <summary>
    /// GIZMO
    /// </summary>
    private void OnDrawGizmos()
    {
        if (Application.isPlaying && HitboxTrans != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(HitboxTrans.position, Vector2.right * DetectDistance * IntegerForMove);
        }
    }


    protected virtual void FixedUpdate()
    {
        if (IsDead) { return; }
        RigidBodyControlAction();
    }

    protected virtual void Update()
    {
        if (IsDead) { return; }
        TickAction();

        bool IsThereEnemy = IsThereEnemyOfFront();
        if (IsThereEnemy)
        {
            CanMoving = false;
            if (AttackTick >= AttackCooldown)
            {
                AttackTick = 0f;
                TryAttack();
            }
        }
        else
        {
            CanMoving = true;
        }
        IsGroundedForDisplayAction();
        IsKnockbackedAction();
    }

    /// <summary>
    /// Refill Stat By Using Properties, Activated After Start()
    /// </summary>
    /// <param name="Properties">EntityProperties</param>
    protected void RefillStat(EntityProperties Properties)
    {
        MaxHp = Properties.Hp;
        Hp = Properties.Hp;
        Speed = Random.Range(Properties.SpeedMin, Properties.SpeedMax);
        DetectDistance = Random.Range(Properties.DetectDistanceMin, Properties.DetectDistanceMax);
        Damage = Properties.Damage;
        AttackCooldown = Properties.AttackCooldown;

        EntityAttackType = Properties.AttackType;
        ProjectilePrefabName = Properties.ProjectilePrefabName;
        ProjectilePrefabObject = prefabMng.GetPrefabByName(ProjectilePrefabName);
    }


    /// <summary>
    /// Hp = MaxHp
    /// </summary>
    private void RefilHp()
    {
        Hp = MaxHp;
    }


    private void InitalAnimatorAction()
    {
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsDead", false);
    }

    private void InitalLoadingAction()
    {
        HitboxTrans = transform.Find("Hitbox").transform;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }








    /// <summary>
    /// 데미지 받는 함수
    /// </summary>
    /// <param name="Damage"></param>
    protected void GetDamage(float Damage)
    {
        if (IsDead == false && Hp > 0)
        {

            Hp -= Damage;

            if (Hp <= 0)
            {
                IsDead = true;
                animator.SetBool("IsDead", true);
            }

        }
    }

    /// <summary>
    /// 넉백 받는 함수
    /// </summary>
    protected void GetKnockback()
    {
        if (IsKnockbacked == false)
        {
            KnockbackForceFieldTick = KnockbackForceFieldTime;
            IsKnockbacked = true;

            rb.AddForce(new Vector2(-IntegerForMove * 4, 4), ForceMode2D.Impulse);

        }
    }

    /// <summary>
    /// 지금 땅위에 서있는지
    /// </summary>
    /// <returns></returns>
    protected bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(HitboxTrans.position, Vector2.down, .5f, LayerMask.GetMask("Ground"));
        if (raycastHit2D && raycastHit2D.collider)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 현재 타격한 적이 있는지 반환
    /// </summary>
    /// <returns></returns>
    protected Transform[] GetFrontEnemies()
    {
        Vector2 Dir;
        string TargetTagName;

        if (IsEnemy)
        {
            Dir = Vector2.left;
            TargetTagName = "Ally";
        }
        else
        {
            Dir = Vector2.right;
            TargetTagName = "Enemy";
        }


        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Dir, DetectDistance, LayerMask.GetMask(TargetTagName));
        int count = hits.Length;

        List<Transform> Trans = new List<Transform>();

        for (int i = 0; i < count; i++)
        {
            RaycastHit2D hit = hits[i];
            bool Compare = hit.transform.CompareTag(TargetTagName);

            if (Compare)
            {
                Trans.Add(hit.transform);
            }
        }
        return Trans.ToArray();
    }

    protected bool IsThereEnemyOfFront()
    {
        Transform[] FrontEnemies = GetFrontEnemies();

        if (FrontEnemies.Length > 0)
        {
            return true;
        }
        return false;
    }

    

    /// <summary>
    /// 캐릭터 물리 관련 (코어)
    /// </summary>
    private void RigidBodyControlAction()
    {
        if (!IsKnockbacked)
        {
            if (CanMoving)
            {
                rb.velocity = new Vector3(Speed * IntegerForMove, rb.velocity.y, 0);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
    }



    /// <summary>
    /// Time.deltaTime 관련된 연산
    /// </summary>
    private void TickAction()
    {
        float DeltaTime = Time.deltaTime;

        KnockbackForceFieldTick -= DeltaTime;
        KnockbackForceFieldTick = Mathf.Clamp(KnockbackForceFieldTick, 0, 1000);

        AttackTick += DeltaTime;
    }

    /// <summary>
    /// IsGrounded 표시를 위해
    /// </summary>
    private void IsGroundedForDisplayAction()
    {
        IsGroundedForDisplay = IsGrounded();
    }

    /// <summary>
    /// IsKnockbacked 다시 꺼주는 작업
    /// </summary>
    private void IsKnockbackedAction()
    {
        if (IsKnockbacked && IsGrounded() && KnockbackForceFieldTick <= 0)
        {
            IsKnockbacked = false;
        }
    }






    /// <summary>
    /// 공격 시도
    /// </summary>
    private void TryAttack()
    {
        animator.SetTrigger("DoAttack");
    }


    /// <summary>
    /// Use This When Have to Attack This Entity
    /// </summary>
    /// <param name="Damage"></param>

    public void GetAttacked(float Damage = 1)
    {
        //Debug.Log("Its Hurt!");
        animator.SetTrigger("Hit");
        GetKnockback();
        GetDamage(Damage);
    }


    protected void MeleeAttack()
    {
        Transform[] FrontEnemies = GetFrontEnemies();
        int length = FrontEnemies.Length;

        for (int i = 0; i < length; i++)
        {
            Transform enemy = FrontEnemies[i];
            EntityCtrl entityCtrl = enemy.GetComponent<EntityCtrl>();
            entityCtrl.GetAttacked();
        }
    }

    protected void ProjectileAttack()
    {
        GameObject New = Instantiate(ProjectilePrefabObject, transform.Find("FirePos").position, Quaternion.identity);
        ProjectileCtrl projectileCtrl = New.GetComponent<ProjectileCtrl>();
        projectileCtrl.SetIsEnemyProjectile(IsEnemy);
        projectileCtrl.WhenOnSpawn();
    }

    protected void AttackAction()
    {
        if (EntityAttackType == eEntityAttackType.Melee)
        {
            MeleeAttack();
        }
        else if (EntityAttackType == eEntityAttackType.Projectile)
        {
            ProjectileAttack();
        }
    }



    /// <summary>
    /// Attack AnimationEvent Reached
    /// </summary>
    public void OnAttackAnimationEvent()
    {
        AttackAction();

    }

    /// <summary>
    /// Dead AnimationEvent Reached
    /// </summary>

    public void OnDeadAnimationEvent()
    {
        Destroy(gameObject);
    }

}
