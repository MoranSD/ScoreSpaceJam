using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class Block : MonoBehaviour
    {
        [HideInInspector] public Vector2 localPlatformPosition;
        [SerializeField] Transform _itemsSpawnPoint;
        [SerializeField] float _itemsSpawnChance;
    }
}
