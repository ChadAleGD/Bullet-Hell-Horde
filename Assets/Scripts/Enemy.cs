using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{

    private Blackboard _blackboard = new();
    private BlackboardKey _isAliveKey;


    private Animator _animator;


    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _coinParent;


    private List<ModuleFactory> _moduleFactories;





    //------------------------------------------------------------------------------------------------//







    private void Awake()
    {

        _animator = GetComponent<Animator>();
    }



    public void Initialize(EnemySO enemyData, Transform playerTransform)
    {
        // Later make some default values such as is alive, speed, health, etc.
        // the blackboard should be able to just generate them based on passed in data
        // the enemy shouldnt have to do this by hand, the blackboard will do the heavy lifting
        _blackboard.ModifyValue(_blackboard.TryGetOrAddKey("EnemySO"), enemyData);
        _blackboard.ModifyValue(_blackboard.TryGetOrAddKey("PlayerTransform"), playerTransform);
        _isAliveKey = _blackboard.TryGetOrAddKey("IsAlive");
        _blackboard.ModifyValue(_isAliveKey, false);


        foreach (var moduleFactory in _moduleFactories)
        {
            var module = moduleFactory.AttachModule(gameObject);
            module.Initialize(_blackboard);
        }


        StartCoroutine(Spawn());
    }


    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(.5f);
        _animator.SetBool("IsAlive", true);
        _blackboard.ModifyValue(_isAliveKey, true);
    }




}