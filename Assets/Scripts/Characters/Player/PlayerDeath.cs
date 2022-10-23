using UnityEngine;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public static event System.Action onDead;
        public static bool isDead { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (isDead) return;

            if (other.CompareTag("KillBox") || other.CompareTag("Zombie"))
            {
                Debug.Log("death");
                onDead?.Invoke();
                isDead = true;
            }
        }
    }
}
