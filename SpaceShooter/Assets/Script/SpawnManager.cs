using Constant;
using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject tripleShotPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject speedupPrefab;

    private bool _stopSpawning;
    private Item item;
    private int itemElementCount = 0;

    private void Awake()
    {
        // 初回のみFindする。
        item = tripleShotPrefab.GetComponent<Item>();
        if (item == null) { Debug.LogError("Item Not Found"); }

        itemElementCount = Enum.GetValues(typeof(Item.ItemType)).Length;
    }

    private void Start()
    {

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnItemRoutine());
    }

    /// <summary>
    /// 敵のスポーンコルーチン
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    private IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning) 
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
        while (!_stopSpawning)
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
}
