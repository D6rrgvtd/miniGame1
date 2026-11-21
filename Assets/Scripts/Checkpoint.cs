
using UnityEngine;

namespace KTC.Scene.Race
{
    public class Checkpoint : MonoBehaviour
    {
        private const string ACTIVATED_MESSAGE =
            "Checkpoint Activated!";

        [Header("êFê›íË")]
        [SerializeField]
        private Color _inactiveColor = Color.red;

        [SerializeField]
        private Color _activatedColor = Color.green;

        private bool _isActivated = false;
        private Renderer _renderer = null;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();

            if (_renderer != null)
            {
                _renderer.material.color = _inactiveColor;
            }
        }

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

            if (_renderer != null)
            {
                _renderer.material.color = _activatedColor;
            }

            Debug.Log(ACTIVATED_MESSAGE);
        }
    }
}
