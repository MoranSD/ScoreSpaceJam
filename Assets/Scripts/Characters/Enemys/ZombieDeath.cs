using UnityEngine;

namespace Zombie
{
    public class ZombieDeath : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("KillBox")) Destroy(this.gameObject);
        }
    }
}
