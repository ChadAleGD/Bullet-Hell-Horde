using System.Collections;
using UnityEngine;



[RequireComponent(typeof(Animator))]
public class EnemyAnimationModule : MonoBehaviour, IEnemyModule
{

    private Animator _animator;



    //------------------------------------------------------------------------------------------------//



    public IEnemyModule Initialize(EnemySO enemyData)
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = enemyData.AnimatorController;

        return this;
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