using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eEntityType
{
    Ally,
    Enemy,
}

public class EntityMng : MonoBehaviour
{

    public static EntityMng Instance;

    [Header("Spawn")]
    public Transform AllySpawnPos;
    public Transform EnemySpawnPos;


    [Header("Vars")]
    public Transform Dynamic;

    public GameObject testAlly;


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
            GameObject newEntity = Instantiate(testAlly, AllySpawnPos.position, Quaternion.identity, Dynamic);
        }
        else if (entityType == eEntityType.Enemy)
        {

        }
    }






















}
