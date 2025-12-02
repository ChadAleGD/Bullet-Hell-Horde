



public interface IEnemyBehavior
{
    void Initialize(Enemy enemy);
    static IEnemyBehavior AttachDefaultBehavior(Enemy host) => host.gameObject.AddComponent<ChaseBehavior>();
}



public interface IEnemyModule
{
    public IEnemyModule Initialize(EnemySO enemyData);
}