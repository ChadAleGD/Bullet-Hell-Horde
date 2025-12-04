using System.Collections;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]
public class ChaseBehavior : MonoBehaviour, IModule
//IDamageable
{

    private Blackboard _blackboard;
    private BlackboardKey _isAliveKey;
    //private BlackboardKey

    private BoxCollider2D _boxCollider;

    // Required modules for chase, without them theres no functionality 




    //------------------------------------------------------------------------------------------------//




    public void Initialize(Blackboard blackboard)
    {
        _blackboard = blackboard;
        _isAliveKey = _blackboard.TryGetOrAddKey("IsAlive");


        //_movementModule = gameObject.AddComponent<EnemyMovementModule>();
        //_enemyAnimationModule = (EnemyAnimationModule)gameObject.AddComponent<EnemyAnimationModule>().Initialize(_host.EnemyData);

        _boxCollider = GetComponent<BoxCollider2D>();

        var playerPosition = _blackboard.TryGetOrAddKey("PlayerPosition");
    }




    /*
    if (!_blackboard.TryGetValue(_isAliveKey, out bool isAlive)) return;
    if (!isAlive) return;


    if (!_blackboard.TryGetValue(_blackboard.TryGetOrAddKey("PlayerTransform"), out Vector3 playerTransform))
    {
        Debug.LogError($"Black board of {gameObject.name} is trying to access PlayerTransform but no key exists!");
        return;
    }

    _blackboard.TryGetOrAddKey("Move");
    //_movementModule.Move(_host.PlayerTransform, _host.MoveSpeed);



    public void TakeDamage(Damage damage) => StartCoroutine(TakeDamageRoutine(damage));

    public IEnumerator TakeDamageRoutine(Damage damage)
    {
        _enemyAnimationModule.Animate("TakingDamage", true);
        yield return null;
        /*
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

    */

}
