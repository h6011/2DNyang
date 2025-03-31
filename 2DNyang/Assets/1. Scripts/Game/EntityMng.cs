using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EntityMng : MonoBehaviour
{
    [Header("MNGS")]
    public static EntityMng Instance;

    [Header("Spawn")]
    public Transform AllySpawnPos;
    public Transform EnemySpawnPos;


    [Header("Vars")]
    public Transform Dynamic;

    [Header("Base Obj")]
    public Transform allyBase;
    public Transform enemyBase;

    [Header("For BaseEffect")]
    private Vector3 SaveAllyBasePos;
    private Vector3 SaveEnemyBasePos;

    private Coroutine allyCoroutine;
    private Coroutine enemyCoroutine;

    private bool NoLongerBaseDamageEffect;
    private bool NoLongerEnemyBaseKnockback;


    [Header("Ally Prefab")]
    public GameObject swordAllyObj;
    public GameObject shieldAllyObj;
    public GameObject bowAllyObj;

    [Header("Enemy Prefab")]
    public GameObject swordEnemyObj;
    public GameObject upgradedSwordEnemyObj;
    public GameObject shieldEnemyObj;
    public GameObject bowEnemyObj;


    private List<GameObject> EntityList = new List<GameObject>();
    public int CurrentAllyCount;
    public int CurrentEnemyCount;

    public int allySpawnCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SaveAllyBasePos = allyBase.position;
        SaveEnemyBasePos = enemyBase.position;
    }

    private void Update()
    {
        
    }



    public void TrySpawnAlly(eAllyType allyType)
    {

        EntityProperties properties = GameSettings.GetAllyProperties(allyType);

        if (LobbyMng.Instance.money >= properties.SpawnCost)
        {
            LobbyMng.Instance.WhenAllySpawn(allyType);

            GameObject obj = null;

            if (allyType == eAllyType.Sword)
            {
                obj = swordAllyObj;
            }
            else if (allyType == eAllyType.Shield)
            {
                obj = shieldAllyObj;
            }
            else if (allyType == eAllyType.Bow)
            {
                obj = bowAllyObj;
            }

            GameObject newAlly = Instantiate(obj, AllySpawnPos.position, Quaternion.identity, Dynamic);
            EntityList.Add(newAlly);
            CurrentAllyCount++;

            allySpawnCount++;
            if (allySpawnCount >= 100)
            {
                allySpawnCount = 0;
            }
        }


        

    }


    public void TrySpawnEnemy(eEnemyType enemyType)
    {
        GameObject obj = null;



        if (enemyType == eEnemyType.Sword)
        {
            obj = swordEnemyObj;
        }
        else if (enemyType == eEnemyType.UpgradedSword)
        {
            obj = upgradedSwordEnemyObj;
        }
        else if (enemyType == eEnemyType.Shield)
        {
            obj = shieldEnemyObj;
        }
        else if (enemyType == eEnemyType.Bow)
        {
            obj = bowEnemyObj;
        }

        GameObject newEnemy = Instantiate(obj, EnemySpawnPos.position, Quaternion.identity, Dynamic);
        EntityList.Add(newEnemy);
        CurrentEnemyCount++;
    }



    private IEnumerator BaseDamageEffectCoroutine(eBaseType baseType, float duration = 0.2f)
    {
        float current = 0f;

        Transform target = null;
        Vector3 savePos = Vector3.zero;

        if (baseType == eBaseType.Ally)
        {
            target = allyBase.Find("Render");
            savePos = SaveAllyBasePos;
        }
        else if (baseType == eBaseType.Enemy)
        {
            target = enemyBase;
            savePos = SaveEnemyBasePos;
        }

        while (current < duration)
        {
            float Process = current / duration;
            float Speed = 10;
            float ReduceAmount = 5;

            float Sined = Mathf.Sin(Process * Speed) / ReduceAmount;

            target.position = savePos + new Vector3(Sined, 0);

            current += Time.deltaTime;

            yield return null;
        }

        target.position = savePos;

        yield return null;
    }


    private IEnumerator BaseDestroyEffectCoroutine(eBaseType baseType, float duration = 4f)
    {
        float current = 0f;

        Transform target = null;
        Vector3 savePos = Vector3.zero;

        if (baseType == eBaseType.Ally)
        {
            target = allyBase.Find("Render");
            savePos = SaveAllyBasePos;
        }
        else if (baseType == eBaseType.Enemy)
        {
            target = enemyBase;
            savePos = SaveEnemyBasePos;
        }

        while (current < duration)
        {
            float Process = current / duration;
            float Speed = 30;
            float ReduceAmount = 5;

            float YAmount = 10f;

            float Sined = Mathf.Sin(Time.time * Speed) / ReduceAmount;
            target.position = savePos + new Vector3(Sined, -YAmount * Process);

            current += Time.deltaTime;

            yield return null;
        }

        //target.position = savePos;

        yield return null;
    }


    public void StopBaseEffectCoroutine(eBaseType baseType)
    {
        if (baseType == eBaseType.Ally)
        {
            if (allyCoroutine != null)
            {
                StopCoroutine(allyCoroutine);
            }

        }
        else if (baseType == eBaseType.Enemy)
        {
            if (enemyCoroutine != null)
            {
                StopCoroutine(enemyCoroutine);
            }
        }
    }

    public void BaseDamagedEffect(eBaseType baseType)
    {
        if (NoLongerBaseDamageEffect)
        {
            return;
        }
        StopBaseEffectCoroutine(baseType);

        if (baseType == eBaseType.Ally)
        {
            allyCoroutine = StartCoroutine(BaseDamageEffectCoroutine(baseType));
        }
        else if (baseType == eBaseType.Enemy)
        {
            enemyCoroutine = StartCoroutine(BaseDamageEffectCoroutine(baseType));
        }
    }

    public void BaseDestroyedEffect(eBaseType baseType)
    {
        NoLongerBaseDamageEffect = true;
        StopBaseEffectCoroutine(baseType);
        Do_GAMEEND_To_AllEntity();

        if (baseType == eBaseType.Ally)
        {
            allyCoroutine = StartCoroutine(BaseDestroyEffectCoroutine(baseType));
        }
        else if (baseType == eBaseType.Enemy)
        {
            enemyCoroutine = StartCoroutine(BaseDestroyEffectCoroutine(baseType));
        }
    }
    

    private void Do_GAMEEND_To_AllEntity()
    {
        int count = EntityList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject entity = EntityList[i];
            EntityCtrl entityCtrl = entity.GetComponent<EntityCtrl>();
            if (entity != null && entityCtrl != null)
            {
                //Debug.Log(entityCtrl);
                entityCtrl.WhenGAME_END();
            }
        }
    }
    public void WhenEntityDead(GameObject EntityObj)
    {
        EntityList.Remove(EntityObj);
        AllyCtrl allyCtrl = EntityObj.GetComponent<AllyCtrl>();
        EnemyCtrl enemyCtrl = EntityObj.GetComponent<EnemyCtrl>();
        if (allyCtrl)
        {
            CurrentAllyCount--;
        }
        else if (enemyCtrl)
        {
            CurrentEnemyCount--;
        }
    }

    public void KnockbackAllies(Vector2 KnockbackAmount)
    {
        int count = EntityList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject entity = EntityList[i];
            AllyCtrl allyCtrl = entity.GetComponent<AllyCtrl>();
            if (entity != null && allyCtrl != null)
            {
                allyCtrl.GetKnockback(KnockbackAmount);
            }
        }
    }















}
