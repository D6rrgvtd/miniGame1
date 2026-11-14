
using UnityEngine;

namespace KTC.Scene.Race
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovingObstacle : MonoBehaviour
    {
        [Header("à⁄ìÆê›íË")]
        [SerializeField]
        private float _moveDistance = 2.5f;

        [SerializeField]
        private float _moveSpeed = 2f;

        private Rigidbody _rigidbody = null;
        private Vector3 _startPosition = Vector3.zero;
        private float _elapsedTime = 0f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;

            _startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            _elapsedTime += Time.fixedDeltaTime;

            float offset = Mathf.Sin(_elapsedTime * _moveSpeed)
                * _moveDistance;

            Vector3 targetPosition =
                _startPosition + Vector3.right * offset;

            _rigidbody.MovePosition(targetPosition);
        }
    }
}
