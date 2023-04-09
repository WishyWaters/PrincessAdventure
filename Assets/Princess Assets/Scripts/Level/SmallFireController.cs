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

        }

        public void LightFire()
        {
            _isLit = true;
            _fireEffect.SetActive(true);
            
        }

        public void Extinguish()
        {
            _isLit = false;
            _fireEffect.SetActive(false);
        }

        public bool IsCorrect()
        {
            return _shouldBeLit == _isLit;
        }
    }
}
