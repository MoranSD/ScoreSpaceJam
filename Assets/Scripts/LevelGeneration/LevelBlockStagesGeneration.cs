using System.Collections.Generic;
using UnityEngine;
using Player;

namespace LevelGeneration
{
    public class LevelBlockStagesGeneration : MonoBehaviour
    {
        [field: SerializeField] public float startHeight { get; private set; }
        [field: SerializeField] public float heightStep { get; private set; }
        public int platformStageCount { get; private set; }

        [SerializeField] float _blockSpawnRate;
        [SerializeField] float _blockSpacing;
        [SerializeField] float _blockSpawnHeightOffset;
        [SerializeField, Min(2)] int _maxOldStagesCount;
        [SerializeField] Block _blockPrefab;

        StagePlatforms _currentPlatformsStage;
        List<StagePlatforms> _oldPlatformStages = new List<StagePlatforms>();

        float _currentSpawnTime;

        Vector2[] _blockPositions = new Vector2[]
        {
            new Vector2(-1, 1),//top left
            new Vector2(1, 1),//top right
            new Vector2(-1, -1),//bottom left
            new Vector2(1, -1),//bottom right
        };

        private void Start()
        {
            platformStageCount = 0;
            _currentSpawnTime = _blockSpawnRate;
            _currentPlatformsStage = new StagePlatforms();
        }
        private void Update()
        {
            if (PlayerDeath.isDead) return;

            _currentSpawnTime -= Time.deltaTime;

            if(_currentSpawnTime <= 0)
            {
                _currentSpawnTime = _blockSpawnRate;
                CreateBlockInCurrentStage();
            }
        }
        void CreateBlockInCurrentStage()
        {
            if(_currentPlatformsStage.blocks.Count >= 4)
            {
                _oldPlatformStages.Add(_currentPlatformsStage);
                if(_oldPlatformStages.Count >= _maxOldStagesCount)
                {
                    MadeBlocksInPlatformStageKinematic(_oldPlatformStages[1]);
                    DestroyBlocksFromPlatformStage(_oldPlatformStages[0]);
                    _oldPlatformStages.RemoveAt(0);
                }

                CreateNewPlatformStage();
                return;
            }

            while (true)
            {
                Vector2 nextBlockPosition = _blockPositions[Random.Range(0, _blockPositions.Length)];

                bool isAlreadyHaveBlockWithThisPosition = false;
                for (int i = 0; i < _currentPlatformsStage.blocks.Count; i++)
                {
                    if (_currentPlatformsStage.blocks[i].localPlatformPosition == nextBlockPosition) isAlreadyHaveBlockWithThisPosition = true;
                }
                if (isAlreadyHaveBlockWithThisPosition)
                {
                    continue;
                }
                else
                {
                    Block nextBlock = CreateBlock(nextBlockPosition);
                    _currentPlatformsStage.blocks.Add(nextBlock);
                    break;
                }
            }
        }
        Block CreateBlock(Vector2 position)
        {
            Block nextBlock = Instantiate(_blockPrefab);
            nextBlock.transform.position = new Vector3(position.x, 0, position.y) * _blockSpacing + (Vector3.up * GetCurrentHeight()) + Vector3.up * _blockSpawnHeightOffset;
            nextBlock.localPlatformPosition = position;
            return nextBlock;
        }
        void CreateNewPlatformStage()
        {
            platformStageCount++;
            _currentPlatformsStage = new StagePlatforms();
            CreateBlockInCurrentStage();
        }
        float GetCurrentHeight() => (startHeight + (heightStep * platformStageCount));
        void MadeBlocksInPlatformStageKinematic(StagePlatforms stage)
        {
            for (int i = stage.blocks.Count - 1; i >= 0; i--)
            {
                stage.blocks[i].GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        void DestroyBlocksFromPlatformStage(StagePlatforms stage)
        {
            for (int i = stage.blocks.Count - 1; i >= 0; i--)
            {
                Destroy(stage.blocks[i].gameObject);
            }
        }
    }
    public class StagePlatforms
    {
        public List<Block> blocks = new List<Block>();
    }
}
