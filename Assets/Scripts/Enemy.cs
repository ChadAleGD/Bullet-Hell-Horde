using System.Collections;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{


    public EnemySO EnemyData;


    private Animator _animator;
    private BoxCollider2D _bcol;


    public float MoveSpeed;

    [SerializeField] private float _maxHealth;
    [SerializeField] public float _health;
    public bool IsAlive = false;




    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _coinParent;



    public Transform PlayerTransform;
    private EnemyBehaviorFactory _behaviorFactory;
    private IEnemyBehavior _behavior;




    //------------------------------------------------------------------------------------------------//







    private void Awake()
    {
        _bcol = GetComponent<BoxCollider2D>();



        _animator = GetComponent<Animator>();
    }



    public void Bootstrap(EnemySO enemyData, Transform playerTransform)
    {
        EnemyData = enemyData;
        PlayerTransform = playerTransform;
        _behaviorFactory = enemyData.BehaviorFactory;

        _maxHealth = enemyData.MaxHealth;
        _health = enemyData.MaxHealth;
        MoveSpeed = enemyData.Speed;


        if (_behaviorFactory == null) _behavior = IEnemyBehavior.AttachDefaultBehavior(this);
        else _behavior = _behaviorFactory.AttachBehavior(this);

        _behavior.Initialize(this);

        StartCoroutine(Spawn());
    }


    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(.5f);
        _animator.SetBool("IsAlive", true);
        IsAlive = true;
    }




}