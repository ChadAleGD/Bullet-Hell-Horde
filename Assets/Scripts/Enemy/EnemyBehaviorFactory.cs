using UnityEngine;



public abstract class EnemyBehaviorFactory : ScriptableObject
{
    public abstract IEnemyBehavior AttachBehavior(Enemy host);
}