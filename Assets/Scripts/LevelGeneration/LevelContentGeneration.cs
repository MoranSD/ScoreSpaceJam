using UnityEngine;
using Things;
using Player;

namespace LevelGeneration
{
    [RequireComponent(typeof(LevelBlockStagesGeneration))]
    public class LevelContentGeneration : MonoBehaviour
    {
        [Header("Spawn settings")]
        [SerializeField] float _spawnRate;
        [SerializeField] float _spawnRadious;
        [SerializeField, Range(0, 100)] float _enemySpawnChance;
        [Header("Content prefabs")]
        [SerializeField] Item[] _items;
        [SerializeField] GameObject _zombiePrefab;

        LevelBlockStagesGeneration _blocksGeneration;
        float _currentSpawnTime;

        private void Start()
        {
            _blocksGeneration = GetComponent<LevelBlockStagesGeneration>();
        }
        private void Update()
        {
            if (PlayerDeath.isDead) return;

            _currentSpawnTime -= Time.deltaTime;

            if(_currentSpawnTime <= 0)
            {
                _currentSpawnTime = _spawnRate;

                float enemySpawnChane = Random.Range(0.1f, 100f);
                float nextContentSpawnHeight = _blocksGeneration.startHeight + (_blocksGeneration.heightStep * _blocksGeneration.platformStageCount);
                Vector3 nextContentPosition = new Vector3(Random.Range(_spawnRadious * -1, _spawnRadious), nextContentSpawnHeight, Random.Range(_spawnRadious * -1, _spawnRadious));

                if (enemySpawnChane <= _enemySpawnChance)
                {
                    Instantiate(_zombiePrefab, nextContentPosition, Quaternion.identity);
                }
                else
                {
                    Item item = Instantiate(_items[Random.Range(0, _items.Length)]);
                    item.transform.position = nextContentPosition;
                }
            }
        }
    }
}
