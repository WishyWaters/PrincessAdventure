using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class CompanionManager : MonoBehaviour
    {
        [SerializeField] List<GameObject> _companionPrefabs;
        [SerializeField] private GameObject _unsummonEffectPrefab;
        [SerializeField] private AudioClip _unsummonSound;

        private CompanionController _summonedCompanionCtrl;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateNextInputs(PrincessInputActions inputs)
        {
            _summonedCompanionCtrl.UpdateNextInputs(inputs);
        }

        public GameObject HandleCompanionActivation(int companionId, Vector3 targetPosition)
        {
            if (_summonedCompanionCtrl == null)
            {
                return ActivateNewSummon(companionId, targetPosition);
            }
            else if (_summonedCompanionCtrl.GetCompanionId() != companionId)
            {
                DestroyCurrentSummon();
                return ActivateNewSummon(companionId, targetPosition);
            }
            else
                return ReactivateCurrentSummon();
        }

        public void SetIgnoreInputs(bool ignore)
        {
            if(_summonedCompanionCtrl != null)
                _summonedCompanionCtrl.SetIngoreInputs(ignore);
        }

        private GameObject ActivateNewSummon(int companionId, Vector3 targetPosition)
        {
            if(_companionPrefabs.Count <= companionId)
            {
                GameObject companion = Instantiate(_companionPrefabs[companionId - 1], targetPosition, new Quaternion());
                _summonedCompanionCtrl = companion.GetComponent<CompanionController>();
                return companion;
            }
            return null;
        }

        public void DestroyCurrentSummon()
        {

            Instantiate(_unsummonEffectPrefab, _summonedCompanionCtrl.gameObject.transform.position, this.transform.rotation);
            SoundManager.SoundInstance.PlayEffectSound(_unsummonSound);

            Destroy(_summonedCompanionCtrl.gameObject);
            _summonedCompanionCtrl = null;
        }

        private GameObject ReactivateCurrentSummon()
        {
            return _summonedCompanionCtrl.gameObject;
        }
    }
}