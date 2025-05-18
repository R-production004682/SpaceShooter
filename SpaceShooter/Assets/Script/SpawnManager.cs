using Constant;
using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject astroidPrefab;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject astroidContainer; 
    [SerializeField] private GameObject tripleShotPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject speedupPrefab;

    private bool stopSpawning;
    private Item item;
    private int itemElementCount = 0;

    private void Awake()
    {
        item = tripleShotPrefab.GetComponent<Item>();
        if (item == null) { Debug.LogError("Item Not Found"); }

        itemElementCount = Enum.GetValues(typeof(Item.ItemType)).Length;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnItemRoutine());
        StartCoroutine(SpawnAstroidRoutine());
    }

    /// <summary>
    /// 全てのスポーンを止める
    /// </summary>
    public void StopAllSpawn()
    {
        stopSpawning = true;
    }


    /// <summary>
    /// 敵のスポーンコルーチン
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    private IEnumerator SpawnEnemyRoutine()
    {
        while (!stopSpawning) 
        {
            var spawnPosition = new Vector3
            (
                UnityEngine.Random.Range(LimitedPosition.LEFT, LimitedPosition.RIGHT),
                LimitedPosition.SPAWN_TOP,
                0
            );

            var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(SpawnObjectTime.ENEMY);
        }
    }

    /// <summary>
    /// アイテムスポーンコルーチン
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    private IEnumerator SpawnItemRoutine()
    {
        while (!stopSpawning)
        {
            var random = UnityEngine.Random.Range(0, itemElementCount); // 0: POWERUP, 1: SHIELD, 2: SPEEDUP
            GameObject selectedItemPrefab = null;

            var spawnPosition = new Vector3
            (
                UnityEngine.Random.Range(LimitedPosition.LEFT, LimitedPosition.RIGHT),
                LimitedPosition.SPAWN_TOP,
                0
            );

            switch (random)
            {
                case 0:
                    item.itemType = Item.ItemType.POWERUP;
                    selectedItemPrefab = tripleShotPrefab;
                    yield return new WaitForSeconds(SpawnObjectTime.BOOSTERS_ITEM); 
                    break;
                case 1:
                    item.itemType = Item.ItemType.SHIELD;
                    selectedItemPrefab = shieldPrefab;
                    yield return new WaitForSeconds(SpawnObjectTime.SHIELD_ITEM); 
                    break;
                case 2:
                    item.itemType = Item.ItemType.SPEEDUP;
                    selectedItemPrefab = speedupPrefab;
                    yield return new WaitForSeconds(SpawnObjectTime.SPEEDUP_ITEM);
                    break;

                default:
                    break;
            }

            if(selectedItemPrefab != null)
            {
                Instantiate(selectedItemPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// 隕石のスポーンコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnAstroidRoutine()
    {
        while(!stopSpawning)
        {
            var spawnPosition = new Vector3
            (
                UnityEngine.Random.Range(LimitedPosition.LEFT, LimitedPosition.RIGHT),
                LimitedPosition.SPAWN_TOP,
                0
            );

            var newAstroid = Instantiate(astroidPrefab, spawnPosition, Quaternion.identity);
            newAstroid.transform.parent = astroidContainer.transform;
            yield return new WaitForSeconds(SpawnObjectTime.ASTRPID);
        }
    }
}
