using Gameplay.Player;
using UnityEngine;


public class EnemyFactory : MonoBehaviour
{


    [SerializeField] private GameObject _enemyBasePrefab;
    [SerializeField] private Transform _playerTransform;



    //------------------------------------------------------------------------------------------------//




    public void GenerateEnemy(EnemySO enemySO, Transform parentTransform)
    {
        var newEnemy = Instantiate(_enemyBasePrefab, parentTransform);
        newEnemy.GetComponent<Enemy>().Bootstrap(enemySO, _playerTransform);
    }




}

