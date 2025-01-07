using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EntityProperties
{
    public float Hp;

    public float DetectDistanceMin;
    public float DetectDistanceMax;

    public float SpeedMin;
    public float SpeedMax;

    public float Damage;
    public float AttackCooldown;

    public eEntityAttackType AttackType;

    /// <summary>
    /// Use When AttackType is Projectile
    /// </summary>
    public float ProjectileSpeed;
}


public class AllyProperties : EntityProperties
{
    public eAllyType AllyType;
}

public class EnemyProperties : EntityProperties
{
    public eEnemyType EnemyType;
}

public class GameSettings : MonoBehaviour
{
    public static List<AllyProperties> AllyPropertiesSettings = new List<AllyProperties>()
    {
        new AllyProperties()
        {
            AllyType = eAllyType.Sword,
            Hp = 10f,

            DetectDistanceMin = 1f,
            DetectDistanceMax = 1.5f,

            SpeedMin = 2f,
            SpeedMax = 2.5f,

            Damage = 1f,
            AttackCooldown = 1.5f,

            AttackType = eEntityAttackType.Melee,
        },
        new AllyProperties()
        {
            AllyType = eAllyType.Shield,
            Hp = 30f,

            DetectDistanceMin = 0.5f,
            DetectDistanceMax = 0.7f,

            SpeedMin = 1.0f,
            SpeedMax = 1.5f,

            Damage = 0.5f,
            AttackCooldown = 2.5f,

            AttackType = eEntityAttackType.Melee,
        },
        new AllyProperties()
        {
            AllyType = eAllyType.Bow,
            Hp = 5f,

            DetectDistanceMin = 4f,
            DetectDistanceMax = 5.5f,

            SpeedMin = 2f,
            SpeedMax = 2.5f,

            Damage = 1.5f,
            AttackCooldown = 1.5f,

            AttackType = eEntityAttackType.Projectile,
            ProjectileSpeed = 5,
        },
    };
    public static List<EnemyProperties> EnemyPropertiesSettings = new List<EnemyProperties>()
    {
        new EnemyProperties()
        {
            EnemyType = eEnemyType.Sword,
            Hp = 10f,

            DetectDistanceMin = 1f,
            DetectDistanceMax = 1.5f,

            SpeedMin = 2f,
            SpeedMax = 2.5f,

            Damage = 1f,
            AttackCooldown = 1.5f,

            AttackType = eEntityAttackType.Melee,
        },
        new EnemyProperties()
        {
            EnemyType = eEnemyType.Shield,
            Hp = 30f,

            DetectDistanceMin = 0.5f,
            DetectDistanceMax = 0.7f,

            SpeedMin = 1.0f,
            SpeedMax = 1.5f,

            Damage = 0.5f,
            AttackCooldown = 2.5f,

            AttackType = eEntityAttackType.Melee,
        },
        new EnemyProperties()
        {
            EnemyType = eEnemyType.Bow,
            Hp = 5f,

            DetectDistanceMin = 4f,
            DetectDistanceMax = 5.5f,

            SpeedMin = 2f,
            SpeedMax = 2.5f,

            Damage = 1.5f,
            AttackCooldown = 1.5f,

            AttackType = eEntityAttackType.Projectile,
            ProjectileSpeed = 5,
        },
    };

    public static AllyProperties GetAllyProperties(eAllyType AllyType)
    {
        int count = AllyPropertiesSettings.Count;
        for (int i = 0; i < count; i++)
        {
            AllyProperties Properties = AllyPropertiesSettings[i];
            if (Properties.AllyType == AllyType)
            {
                return Properties;
            }
        }
        return null;
    }

    public static EnemyProperties GetEnemyProperties(eEnemyType EnemyType)
    {
        int count = EnemyPropertiesSettings.Count;
        for (int i = 0; i < count; i++)
        {
            EnemyProperties Properties = EnemyPropertiesSettings[i];
            if (Properties.EnemyType == EnemyType)
            {
                return Properties;
            }
        }
        return null;
    }


}
