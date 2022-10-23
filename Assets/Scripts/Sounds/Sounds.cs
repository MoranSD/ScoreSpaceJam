using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Player.Inventory;
using Things;
using LevelGeneration;

namespace Sound
{
    public class Sounds : PlayerInventoryItemLocator
    {
        [SerializeField, Range(0, 1)] float _volume;
        [Header("Item pick up sounds")]
        [SerializeField] ItemPickUpSounds _itemPickUpSounds;
        [Header("Item hold sounds")]
        [SerializeField] ItemHoldSounds _itemHolds;
        [Header("Level generation sounds")]
        [SerializeField] LevelSounds _levelSounds;
        [Header("Player sounds")]
        [SerializeField] PlayerSounds _playerSounds;

        protected override void OnEnable()
        {
            base.OnEnable();

            LevelBlockStagesGeneration.onSpawnBlock += OnGenerationSpawnBlock;
            LevelBlockStagesGeneration.onEndStage += OnPlayerEndStage;
            PlayerMovement.onJump += OnPlayerJump;
            PlayerDeath.onDead += OnPlayerDead;
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            LevelBlockStagesGeneration.onSpawnBlock -= OnGenerationSpawnBlock;
            LevelBlockStagesGeneration.onEndStage -= OnPlayerEndStage;
            PlayerMovement.onJump -= OnPlayerJump;
            PlayerDeath.onDead -= OnPlayerDead;
        }
        void OnGenerationSpawnBlock() => PlaySound(_levelSounds.spawnBlock);
        void OnPlayerEndStage() => PlaySound(_levelSounds.doneStage);
        void OnPlayerJump() => PlaySound(_playerSounds.jump);
        void OnPlayerDead() => PlaySound(_playerSounds.death);
        protected override void OnPlayerTakeItem(ItemType type)
        {
            switch (type)
            {
                case ItemType.Coin:
                    PlaySound(_itemPickUpSounds.coin);
                    break;
                case ItemType.Pistol:
                    PlaySound(_itemPickUpSounds.pistol);
                    _itemHolds.pistol.Play(_volume);
                    break;
                case ItemType.SpeedBoost:
                    PlaySound(_itemPickUpSounds.speedBoost);
                    _itemHolds.speedBoost.Play(_volume);
                    break;
            }
        }
        protected override void OnPlayerDropItem(ItemType type)
        {
            switch (type)
            {
                case ItemType.Pistol:
                    _itemHolds.pistol.Stop();
                    break;
                case ItemType.SpeedBoost:
                    _itemHolds.speedBoost.Stop();
                    break;
            }
        }
        void PlaySound(AudioClip clip)
        {
            GameObject audio = new GameObject();
            audio.transform.SetParent(Camera.main.transform);
            audio.transform.localPosition = Vector3.zero;
            AudioSource source = audio.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = _volume;
            source.Play();
            Destroy(audio, clip.length);
        }
    }
    [System.Serializable]
    public struct PlayerSounds
    {
        public AudioClip jump;
        public AudioClip death;
    }
    [System.Serializable]
    public struct LevelSounds
    {
        public AudioClip spawnBlock;
        public AudioClip doneStage;
    }
    [System.Serializable]
    public struct ItemPickUpSounds
    {
        public AudioClip coin;
        public AudioClip pistol;
        public AudioClip speedBoost;
    }
    [System.Serializable]
    public class ItemHoldSounds
    {
        public ItemHoldSound pistol;
        public ItemHoldSound speedBoost;
    }
    [System.Serializable]
    public class ItemHoldSound
    {
        public AudioClip sound;
        private AudioSource _source;

        public void Play(float volume)
        {
            if (sound == null) return;
            if(_source == null)
            {
                GameObject audio = new GameObject();
                audio.transform.SetParent(Camera.main.transform);
                audio.transform.localPosition = Vector3.zero;
                AudioSource source = audio.AddComponent<AudioSource>();
                source.clip = sound;
                source.volume = volume;
                _source = source;
            }

            _source.Play();
        }
        public void Stop()
        {
            if (_source == null) return;
            _source.Stop();
        }
    }
}
