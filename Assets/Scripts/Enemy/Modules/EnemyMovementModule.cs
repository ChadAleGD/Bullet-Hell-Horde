using System.Collections;
using UnityEngine;



[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovementModule : MonoBehaviour, IModule
{

    private Blackboard _blackboard;

    private Rigidbody2D _rb;



    private bool _inKnockback;
    private readonly float _knockbackTime = .2f;



    //------------------------------------------------------------------------------------------------//



    public void Initialize(Blackboard blackboard)
    {
        _rb = GetComponent<Rigidbody2D>();

        _blackboard = blackboard;
    }



    //------------------------------------------------------------------------------------------------//



    public void Move(Transform target, float moveSpeed)
    {
        if (_inKnockback) return;

        var displacement = (target.position - transform.position).normalized;
        _rb.linearVelocity = new Vector2(displacement.x * moveSpeed, displacement.y * moveSpeed);
    }





    public IEnumerator Knockback(Transform target, float pushForce)
    {
        _inKnockback = true;


        var displacement = (transform.position - target.position).normalized;
        _rb.linearVelocity = displacement * pushForce;

        yield return new WaitForSeconds(_knockbackTime);

        _rb.linearVelocity = Vector3.zero;

        _inKnockback = false;
    }

}