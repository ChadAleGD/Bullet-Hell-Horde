using System;
using System.Collections.Generic;
using UnityEngine;




[Serializable]
public struct RoundData
{
    public int TotalEnemies;
    public int ActiveEnemyMax;
    public int MaxEnemyBurst;
}


[Serializable]
public struct EnemyConfig
{
    public float Health;
    public float Speed;
    public float BaseDamage;
    public RuntimeAnimatorController AnimationController;
}


public interface IEnemyBehavior
{
    void Initialize(Enemy enemy);
    static IEnemyBehavior AttachDefaultBehavior(Enemy host) => host.gameObject.AddComponent<ChaseBehavior>();
}


public abstract class EnemyBehaviorFactory : ScriptableObject
{
    public abstract IEnemyBehavior AttachBehavior(Enemy host);
}