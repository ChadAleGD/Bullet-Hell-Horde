using UnityEngine;


public class EnemySpawner : MonoBehaviour
{


    [SerializeField] private GameObject _enemyBasePrefab;
    [SerializeField] private Transform _playerTransform;



    //------------------------------------------------------------------------------------------------//




    public void SpawnEnemy(EnemySO enemySO, Transform parentTransform)
    {
        var newEnemy = Instantiate(_enemyBasePrefab, parentTransform);
        newEnemy.GetComponent<Enemy>().Initialize(enemySO, _playerTransform);
    }




}

