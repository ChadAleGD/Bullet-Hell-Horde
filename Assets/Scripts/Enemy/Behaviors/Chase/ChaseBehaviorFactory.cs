using UnityEngine;


[CreateAssetMenu(fileName = "ChaseBehavior", menuName = "Enemy Behavior/ChaseBehavior")]
public class ChaseBehaviorFactory : EnemyBehaviorFactory
{
    public override IEnemyBehavior AttachBehavior(Enemy host) => host.gameObject.AddComponent<ChaseBehavior>();
}