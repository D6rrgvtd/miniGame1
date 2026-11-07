
using TMPro;
using UnityEngine;

namespace KTC.Scene.Race
{
    public class GoalController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _clearPanel = null;

        [SerializeField]
        private TMP_Text _clearText = null;

        private bool _isCleared = false;

        private void Start()
        {
            _clearPanel.SetActive(false);
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

            _clearText.text = ConstRace.CLEAR_MESSAGE;
            _clearPanel.SetActive(true);
        }
    }
}
