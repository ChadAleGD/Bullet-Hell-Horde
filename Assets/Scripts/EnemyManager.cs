using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyManager : MonoBehaviour
{

    [SerializeField] private EnemyBehaviorFactory _enemyFactory;


    private int _enemiesSpawned = 0;
    private int _enemiesAlive = 0;


    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<Transform> _spawnPositions = new();


    private WaitForSeconds _spawnCheckTick = new(3f);
    private WaitForSeconds _spawnBurstTick = new(.2f);

    private bool _isSpawning = false;

    public EnemySO enemySO;

    public Transform player;



    //------------------------------------------------------------------------------------------------//



    private void OnEnable()
    {
        EventBus.Subscribe(EventType.OnRoundStart, data => StartRound((RoundData)data));
        EventBus.Subscribe(EventType.OnEnemyDied, EnemyDied);
    }



    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.OnRoundStart, data => StartRound((RoundData)data));
        EventBus.Unsubscribe(EventType.OnEnemyDied, EnemyDied);
    }



    //------------------------------------------------------------------------------------------------//




    private void StartRound(RoundData roundData)
    {
        _enemiesSpawned = 0;
        _enemiesAlive = 0;

        StartCoroutine(RoundRoutine(roundData));
    }



    private IEnumerator RoundRoutine(RoundData roundData)
    {
        while (_enemiesSpawned < roundData.TotalEnemies)
        {
            Debug.Log($"Enemies alive {_enemiesAlive}, enemies spawned {_enemiesSpawned}");
            if (!_isSpawning && _enemiesAlive < roundData.ActiveEnemyMax) StartCoroutine(DetermineBurst(roundData));
            yield return _spawnCheckTick;
        }

        yield return new WaitUntil(() => _enemiesAlive == 0);

        EventBus.Publish(EventType.OnRoundEnd);
    }



    private IEnumerator DetermineBurst(RoundData roundData)
    {
        _isSpawning = true;

        var spawnAmount = roundData.ActiveEnemyMax - _enemiesAlive;
        var maxBurst = roundData.MaxEnemyBurst;

        if (_enemiesSpawned + spawnAmount > roundData.TotalEnemies) spawnAmount = roundData.TotalEnemies - _enemiesSpawned;



        if (spawnAmount > maxBurst)
        {
            var remainingSpawnCount = spawnAmount;

            for (int i = 0; i < Mathf.Ceil(spawnAmount / maxBurst); i++)
            {
                if (remainingSpawnCount > maxBurst)
                {
                    yield return StartCoroutine(SpawnBurst(maxBurst));
                    remainingSpawnCount -= maxBurst;
                }
                else yield return StartCoroutine(SpawnBurst(remainingSpawnCount));

                yield return _spawnBurstTick;
            }

        }
        else yield return StartCoroutine(SpawnBurst(spawnAmount));

        _isSpawning = false;
    }



    private IEnumerator SpawnBurst(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            _enemiesAlive++;
            _enemiesSpawned++;

            var randomSpawnIndex = Random.Range(0, _spawnPositions.Count);
            var newEnemy = Instantiate(_enemyPrefab, _spawnPositions[randomSpawnIndex]);
            newEnemy.GetComponent<Enemy>().Bootstrap(enemySO, player);

            yield return _spawnBurstTick;
        }
    }


    public void EnemyDied() => _enemiesAlive--;



}

