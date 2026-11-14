
using UnityEngine;

namespace KTC.Scene.Race
{
    public class Checkpoint : MonoBehaviour
    {
        private bool _isActivated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (_isActivated)
            {
                return;
            }

            PlayerController playerController =
                other.GetComponent<PlayerController>();

            if (playerController == null)
            {
                return;
            }

            playerController.SetCheckpoint(transform.position);
            _isActivated = true;
        }
    }
}
