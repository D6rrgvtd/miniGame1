
using UnityEngine;

namespace KTC.Scene.Race
{
    [RequireComponent(typeof(Rigidbody))]
    public class RotatingObstacle : MonoBehaviour
    {
        private const float ROTATION_SPEED = 90f;

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
                ROTATION_SPEED * Time.fixedDeltaTime,
                0f
            );

            _rigidbody.MoveRotation(
                _rigidbody.rotation * rotation
            );
        }
    }
}
