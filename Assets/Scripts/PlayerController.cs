
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KTC.Scene.Race
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        private const float MOVE_SPEED = 6f;
        private const float JUMP_FORCE = 7f;

        private Rigidbody _rigidbody = null;
        private Vector3 _startPosition = Vector3.zero;
        private bool _isGrounded = false;
        private bool _isRespawning = false;
        private bool _isCleared = false;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _startPosition = transform.position;
        }

        private void Update()
        {
            if (Keyboard.current == null || _isRespawning || _isCleared)
            {
                return;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded)
            {
                _rigidbody.AddForce(
                    Vector3.up * JUMP_FORCE,
                    ForceMode.Impulse
                );

                _isGrounded = false;
            }
        }

        private void FixedUpdate()
        {
            if (Keyboard.current == null || _isRespawning || _isCleared)
            {
                return;
            }

            float horizontal = 0f;
            float vertical = 0f;

            if (Keyboard.current.aKey.isPressed)
            {
                horizontal -= 1f;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                horizontal += 1f;
            }

            if (Keyboard.current.sKey.isPressed)
            {
                vertical -= 1f;
            }

            if (Keyboard.current.wKey.isPressed)
            {
                vertical += 1f;
            }

            Vector3 movement = new Vector3(
                horizontal,
                0f,
                vertical
            ).normalized;

            Vector3 velocity = _rigidbody.linearVelocity;
            velocity.x = movement.x * MOVE_SPEED;
            velocity.z = movement.z * MOVE_SPEED;

            _rigidbody.linearVelocity = velocity;
        }

        private void OnCollisionStay(Collision collision)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    _isGrounded = true;
                    return;
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            _isGrounded = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "FallZone" && !_isRespawning)
            {
                StartCoroutine(RespawnAsync());
            }
        }

        private IEnumerator RespawnAsync()
        {
            _isRespawning = true;

            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            yield return new WaitForSeconds(ConstRace.RESPAWN_DELAY);

            _rigidbody.position = new Vector3(
                _startPosition.x,
                ConstRace.RESPAWN_HEIGHT,
                _startPosition.z
            );

            _rigidbody.rotation = Quaternion.identity;
            _isGrounded = false;
            _isRespawning = false;
        }
        public void CompleteRace()
        {
            _isCleared = true;

            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
   

    }
