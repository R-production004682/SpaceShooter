using Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyContainer;

    private bool _stopSpawning;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }


    private IEnumerator SpawnRoutine()
    {
        while (!_stopSpawning) 
        {
            var spawnPosition = 
                new Vector3(
                    Random.Range(LimitedPosition.LEFT, LimitedPosition.RIGHT),
                    LimitedPosition.SPAWN_TOP,
                    0);

            var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(ReSpawn.TIME);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
