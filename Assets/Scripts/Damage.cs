using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Normal, Laser }

public struct DamageInfo
{
    public DamageType type;
    public Object damageSource;

}

[System.Serializable]
public struct Damage
{
    public int amount;

    public DamageInfo info;

    public static float TakeDamage(ref Health health, int damageAmount)
    {
        health.amount -= damageAmount;
        return health.amount;
    }
}

[System.Serializable]
public struct Health
{
    public int amount;

    public static float AddHealth(Health health, int healthAmount)
    {
        health.amount += healthAmount;
        return health.amount;
    }
}