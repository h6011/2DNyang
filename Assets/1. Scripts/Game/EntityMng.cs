using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;




public class EntityMng : MonoBehaviour
{

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



    [Header("Ally 아군")]
    public GameObject swordAllyObj;
    public GameObject shieldAllyObj;
    public GameObject bowAllyObj;

    [Header("Enemy 적군")]
    public GameObject swordEnemyObj;
    public GameObject shieldEnemyObj;
    public GameObject bowEnemyObj;



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
        }


        

    }


    public void TrySpawnEnemy(eEnemyType enemyType)
    {
        GameObject obj = null;



        if (enemyType == eEnemyType.Sword)
        {
            obj = swordEnemyObj;
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
    }



    private IEnumerator BaseDamageEffectCoroutine(eBaseType baseType)
    {
        float duration = 0.2f;
        float current = 0f;
        Transform target = null;
        Vector3 savePos = Vector3.zero;

        if (baseType == eBaseType.Ally)
        {
            target = allyBase;
            savePos = SaveAllyBasePos;
        }
        else if (baseType == eBaseType.Enemy)
        {
            target = enemyBase;
            savePos = SaveEnemyBasePos;
        }

        while (current < duration)
        {
            float process = (duration - current) / duration;
            target.position = savePos + new Vector3(Mathf.Sin(process * 10) / 5, 0);
            current += Time.deltaTime;
            yield return null;
        }

        target.position = savePos;

        yield return null;
    }

    public void BaseDamagedEffect(eBaseType baseType)
    {
        if (baseType == eBaseType.Ally)
        {
            if (allyCoroutine != null)
            {
                StopCoroutine(allyCoroutine);
            }
            allyCoroutine = StartCoroutine(BaseDamageEffectCoroutine(baseType));
        }
        else if (baseType == eBaseType.Enemy)
        {
            if (enemyCoroutine != null)
            {
                StopCoroutine(enemyCoroutine);
            }
            enemyCoroutine = StartCoroutine(BaseDamageEffectCoroutine(baseType));
        }
    }



















}
