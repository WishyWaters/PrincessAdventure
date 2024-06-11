using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class SmallFireController : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private GameObject _fireEffect;
        [SerializeField] private bool _shouldBeLit;
        [SerializeField] private bool _isLit;

        [SerializeField] private PuzzleCheckController _puzzleCtrl;

        [SerializeField] private float _igniteTimer;

        private float _igniteCountDown = 0;

        // Start is called before the first frame update
        void Start()
        {
            if (_isLit)
                LightFire();
            else
                Extinguish();
        }

        // Update is called once per frame
        void Update()
        {
            if (_igniteCountDown <= 0)
                return;

            _igniteCountDown -= Time.deltaTime;

            if (_igniteCountDown <= 0)
                LightFire();

        }

        public void LightFire()
        {
            _isLit = true;
            _fireEffect.SetActive(true);

            if(_puzzleCtrl != null)
                _puzzleCtrl.CheckPuzzle();
            
        }

        public void Extinguish()
        {
            _isLit = false;
            _fireEffect.SetActive(false);

            if (_puzzleCtrl != null)
                _puzzleCtrl.CheckPuzzle();

            if (_igniteTimer > 0f)
                _igniteCountDown = _igniteTimer;

        }

        public bool IsCorrect()
        {
            return _shouldBeLit == _isLit;
        }


    }
}
