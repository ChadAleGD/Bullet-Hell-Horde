using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public struct RoundData
{
    public EnemySO EnemyType;
    public int EnemyMaxActiveAmount;
    public int EnemyTotalAmount;
    public int MaxSpawnBurstAmount;
}






public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<RoundData> _roundsData = new();
    private int _roundsIndex = 0;
    private RoundData _currentRoundData;


    [SerializeField] private float _waveSpawnBuffer = 0;
    private WaitForSeconds _waitForWaveBuffer;

    private readonly WaitForSeconds _spawnCheckTick = new(1.5f);
    private readonly WaitForSeconds _spawnBurstTick = new(.2f);


    private int _enemiesSpawned = 0;
    private int _enemiesAlive = 0;


    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<Transform> _spawnPositions = new();



    [SerializeField] private EnemyFactory _enemyFactory;



    //------------------------------------------------------------------------------------------------//



    private void Awake()
    {
        _waitForWaveBuffer = new WaitForSeconds(_waveSpawnBuffer);
    }



    private void OnEnable()
    {
        EventBus.Subscribe(EventType.OnEnemyDied, EnemyDied);
        EventBus.Subscribe(EventType.OnRoundStart, StartRound);
    }



    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.OnEnemyDied, EnemyDied);
        EventBus.Unsubscribe(EventType.OnRoundStart, StartRound);
    }



    //------------------------------------------------------------------------------------------------//




    private void StartRound()
    {
        _enemiesSpawned = 0;
        _enemiesAlive = 0;

        if (_roundsData.Count > _roundsIndex) _currentRoundData = _roundsData[_roundsIndex];
        else return;


        StartCoroutine(RoundRoutine());
    }



    // Entry and main loop
    private IEnumerator RoundRoutine()
    {
        yield return _waitForWaveBuffer;



        while (true)
        {
            if (!AreEnemiesRemaining()) break;


            yield return StartCoroutine(SpawnBurst(DetermineBurst()));


            yield return _spawnCheckTick;
        }


        yield return new WaitUntil(() => _enemiesAlive == 0);

        EventBus.Publish(EventType.OnRoundEnd);
    }



    private bool AreEnemiesRemaining()
    {
        if (_enemiesSpawned == _currentRoundData.EnemyTotalAmount) return false;
        return true;
    }



    private int DetermineBurst()
    {
        var enemySpawnAmountNeeded = _currentRoundData.EnemyMaxActiveAmount - _enemiesAlive;

        if (enemySpawnAmountNeeded > _currentRoundData.MaxSpawnBurstAmount) return _currentRoundData.MaxSpawnBurstAmount;
        else return enemySpawnAmountNeeded;
    }



    private IEnumerator SpawnBurst(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            _enemiesAlive++;
            _enemiesSpawned++;

            var randomSpawnIndex = UnityEngine.Random.Range(0, _spawnPositions.Count);
            _enemyFactory.GenerateEnemy(_currentRoundData.EnemyType, _spawnPositions[randomSpawnIndex]);

            yield return _spawnBurstTick;
        }
    }


    public void EnemyDied() => _enemiesAlive--;



}

