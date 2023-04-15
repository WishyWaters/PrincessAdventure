using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class WeightSwitchController : MonoBehaviour
    {
        [SerializeField] private GameObject _showOnDown;
        [SerializeField] private GameObject _showOnUp;
        [SerializeField] private AudioClip _onDownClip;
        [SerializeField] private AudioClip _onUpClip;

        [SerializeField] private PuzzleCheckController _puzzleCtrl;
        [SerializeField] private AffectedObjectController _affectedCtrl;

        private Collider2D _currentWeight;

        // Start is called before the first frame update
        void Start()
        {
            _showOnDown.SetActive(false);
            _showOnUp.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SwitchDown()
        {
            _showOnDown.SetActive(true);
            _showOnUp.SetActive(false);
            SoundManager.SoundInstance.PlayEffectSound(_onDownClip);

            if (_puzzleCtrl != null)
                _puzzleCtrl.CheckPuzzle();

            if (_affectedCtrl != null)
                _affectedCtrl.ToggleTheObject();
        }

        private void SwitchUp()
        {
            _showOnDown.SetActive(false);
            _showOnUp.SetActive(true);
            SoundManager.SoundInstance.PlayEffectSound(_onUpClip);


            if (_affectedCtrl != null)
                _affectedCtrl.ToggleTheObject();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.layer == 6)
            {
                _currentWeight = collision;
                SwitchDown();

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(_currentWeight == collision)
            { 
                SwitchUp();
                _currentWeight = null;
            }
        }
    }
}