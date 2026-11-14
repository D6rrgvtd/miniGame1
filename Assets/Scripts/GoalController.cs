
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KTC.Scene.Race
{
    public class GoalController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _clearPanel = null;

        [SerializeField]
        private TMP_Text _clearText = null;

        [SerializeField]
        private TMP_Text _retryText = null;

        private bool _isCleared = false;

        private void Start()
        {
            _clearPanel.SetActive(false);
            _clearText.text = ConstRace.CLEAR_MESSAGE;
            _retryText.text = ConstRace.RETRY_MESSAGE;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isCleared)
            {
                return;
            }

            PlayerController playerController =
                other.GetComponent<PlayerController>();

            if (playerController == null)
            {
                return;
            }

            _isCleared = true;

            playerController.CompleteRace();

            _clearPanel.SetActive(true);
        }

        public void RetryGame()
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}
