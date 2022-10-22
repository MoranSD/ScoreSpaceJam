using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombie
{
    [RequireComponent(typeof(Animator))]
    public class ZombieAnimation : MonoBehaviour
    {
        [SerializeField] ZombieMovement _zombieMovement;

        Animator _animator;

        private void Start() => _animator = GetComponent<Animator>();

        private void Update()
        {
            _animator.SetBool("isRun", _zombieMovement.moveDirection != Vector3.zero);
        }
    }
}
