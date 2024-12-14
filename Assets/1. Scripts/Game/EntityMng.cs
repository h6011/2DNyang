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

    public GameObject swordEntityObj;
    public GameObject shieldEntityObj;
    public GameObject bowEntityObj;



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



    public void SpawnEntity(eEntityType entityType)
    {
        if (entityType == eEntityType.Ally)
        {
            //GameObject newEntity = Instantiate(testAlly, AllySpawnPos.position, Quaternion.identity, Dynamic);
        }
        else if (entityType == eEntityType.Enemy)
        {

        }
    }

    public void TrySpawnAlly(eAllyType allyType)
    {
        if (allyType == eAllyType.Sword)
        {
            GameObject newEntity = Instantiate(swordEntityObj, AllySpawnPos.position, Quaternion.identity, Dynamic);
        }
        else if (allyType == eAllyType.Shield)
        {
            GameObject newEntity = Instantiate(shieldEntityObj, AllySpawnPos.position, Quaternion.identity, Dynamic);
        }
        else if (allyType == eAllyType.Bow)
        {
            GameObject newEntity = Instantiate(bowEntityObj, AllySpawnPos.position, Quaternion.identity, Dynamic);
        }
    }






















}
