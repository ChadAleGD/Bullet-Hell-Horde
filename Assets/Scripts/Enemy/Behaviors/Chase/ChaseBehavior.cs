using System.Collections;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]
public class ChaseBehavior : MonoBehaviour, IEnemyBehavior, IDamageable
{
    private Enemy _host;


    private BoxCollider2D _boxCollider;

    // Required modules for chase, without them theres no functionality 
    private EnemyMovementModule _movementModule;
    private EnemyAnimationModule _enemyAnimationModule;




    //------------------------------------------------------------------------------------------------//




    public void Initialize(Enemy enemy)
    {
        _host = enemy;

        _movementModule = (EnemyMovementModule)gameObject.AddComponent<EnemyMovementModule>().Initialize(_host.EnemyData);
        _enemyAnimationModule = (EnemyAnimationModule)gameObject.AddComponent<EnemyAnimationModule>().Initialize(_host.EnemyData);

        _boxCollider = GetComponent<BoxCollider2D>();
    }




    private void FixedUpdate()
    {
        if (!_host.IsAlive) return;

        _movementModule.Move(_host.PlayerTransform, _host.MoveSpeed);
    }


    public void TakeDamage(Damage damage) => StartCoroutine(TakeDamageRoutine(damage));

    public IEnumerator TakeDamageRoutine(Damage damage)
    {
        _enemyAnimationModule.Animate("TakingDamage", true);

        _host._health--;
        yield return StartCoroutine(_movementModule.Knockback(_host.PlayerTransform, damage.PushForce));



        if (_host._health <= 0)
        {
            _host.IsAlive = false;

            _boxCollider.enabled = false;
            yield return _enemyAnimationModule.Animate("IsAlive", false);
            // Instantiate(_coinPrefab, transform.position, Quaternion.identity, _coinParent);

            EventBus.Publish(EventType.OnEnemyDied);
            Destroy(gameObject);

        }
    }


}
