
using UnityEngine;

namespace KTC.Scene.Race
{
    public class JumpPad : MonoBehaviour
    {
        [Header("ÉWÉÉÉìÉvê›íË")]
        [SerializeField]
        private float _jumpForce = 15f;

        private void OnCollisionEnter(Collision collision)
        {
            PlayerController playerController =
                collision.gameObject.GetComponent<PlayerController>();

            if (playerController == null)
            {
                return;
            }

            Rigidbody playerRigidbody =
                collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody == null)
            {
                return;
            }

            Vector3 velocity = playerRigidbody.linearVelocity;
            velocity.y = 0f;
            playerRigidbody.linearVelocity = velocity;

            playerRigidbody.AddForce(
                Vector3.up * _jumpForce,
                ForceMode.Impulse
            );
        }
    }
}
