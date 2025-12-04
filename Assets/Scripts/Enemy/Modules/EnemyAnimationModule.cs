using System.Collections;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class EnemyAnimationModule : MonoBehaviour, IModule
{
    private Blackboard _blackboard;

    private BlackboardKey _enemySOKey;

    private Animator _animator;



    //------------------------------------------------------------------------------------------------//



    public void Initialize(Blackboard blackboard)
    {
        _animator = GetComponent<Animator>();

        _blackboard = blackboard;
        _enemySOKey = _blackboard.TryGetOrAddKey("EnemySO");

        if (_blackboard.TryGetValue(_enemySOKey, out EnemySO so)) _animator.runtimeAnimatorController = so.AnimatorController;
        else Debug.LogError($"EnemySO controller for {gameObject} was not found");
    }



    //------------------------------------------------------------------------------------------------//



    public Coroutine Animate(string boolRef, bool state) => StartCoroutine(AnimationRoutine(boolRef, state));

    private IEnumerator AnimationRoutine(string boolRef, bool state)
    {
        _animator.SetBool(boolRef, state);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length - .5f);
        _animator.SetBool(boolRef, !state);
    }

}