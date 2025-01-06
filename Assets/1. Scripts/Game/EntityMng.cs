using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EntityMng : MonoBehaviour
{

    public static EntityMng Instance;

    [Header("Spawn")]
    public Transform AllySpawnPos;
    public Transform EnemySpawnPos;


    [Header("Vars")]
    public Transform Dynamic;

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
        
    }


    public void TrySpawnAlly(eAllyType allyType)
    {
        GameObject obj = null;

        if (allyType == eAllyType.Sword)
        {
            obj = swordAllyObj;
        }
        else if (allyType == eAllyType.Shield)
        {
            obj = shieldEnemyObj;
        }
        else if (allyType == eAllyType.Bow)
        {
            obj = bowAllyObj;
        }

        GameObject newAlly = Instantiate(obj, AllySpawnPos.position, Quaternion.identity, Dynamic);

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






















}
