
using UnityEngine;

namespace KTC.Scene.Race
{
    [RequireComponent(typeof(Rigidbody))]
    public class RotatingObstacle : MonoBehaviour
    {
        [Header("âÒì]ê›íË")]
        [SerializeField]
        private float _rotationSpeed = 90f;

        [Header("êÅÇ´îÚÇŒÇµê›íË")]
        [SerializeField]
        private float _knockbackForce = 18f;

        [SerializeField]
        private float _upwardForce = 6f;

        private Rigidbody _rigidbody = null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        private void FixedUpdate()
        {
            Quaternion rotation = Quaternion.Euler(
                0f,
                _rotationSpeed * Time.fixedDeltaTime,
                0f
            );

            _rigidbody.MoveRotation(
                _rigidbody.rotation * rotation
            );
        }

        private void OnCollisionEnter(Collision collision)
        {
            Rigidbody playerRigidbody =
                collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody == null)
            {
                return;
            }

            PlayerController playerController =
                collision.gameObject.GetComponent<PlayerController>();

            if (playerController == null)
            {
                return;
            }

            Vector3 knockbackDirection =
                playerRigidbody.position - _rigidbody.position;

            knockbackDirection.y = 0f;

            if (knockbackDirection.sqrMagnitude > 0f)
            {
                knockbackDirection.Normalize();
            }
            else
            {
                knockbackDirection = Vector3.forward;
            }

            Vector3 knockback =
                knockbackDirection * _knockbackForce
                + Vector3.up * _upwardForce;

            playerRigidbody.AddForce(
                knockback,
                ForceMode.Impulse
            );
        }
    }
}
