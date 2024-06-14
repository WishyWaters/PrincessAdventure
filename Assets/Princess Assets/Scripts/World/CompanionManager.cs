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
        [SerializeField] private GameObject _fireEffectPrefab;
        [SerializeField] private AudioClip _fireSound;

        [SerializeField] private AudioClip _slimeSound;
        [SerializeField] private AudioClip _rabbitSound;
        [SerializeField] private AudioClip _batSound;


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
            if(companionId <= _companionPrefabs.Count)
            {
                GameObject companion = Instantiate(_companionPrefabs[companionId - 1], targetPosition, new Quaternion());
                _summonedCompanionCtrl = companion.GetComponent<CompanionController>();
                return companion;
            }
            return null;
        }

        public void DestroyCurrentSummon()
        {
            int compId = _summonedCompanionCtrl.GetCompanionId();

            if (compId == 1)
            {
                Instantiate(_unsummonEffectPrefab, _summonedCompanionCtrl.gameObject.transform.position, this.transform.rotation);
                SoundManager.SoundInstance.PlayEffectSound(_slimeSound);
                SoundManager.SoundInstance.PlayEffectSound(_unsummonSound);

            }
            else if (compId == 2)
            {
                Instantiate(_fireEffectPrefab, _summonedCompanionCtrl.gameObject.transform.position, this.transform.rotation);
                SoundManager.SoundInstance.PlayEffectSound(_rabbitSound);
                SoundManager.SoundInstance.PlayEffectSound(_fireSound);
            }
            else if (compId == 3)
            {
                Instantiate(_unsummonEffectPrefab, _summonedCompanionCtrl.gameObject.transform.position, this.transform.rotation);
                SoundManager.SoundInstance.PlayEffectSound(_batSound);
                SoundManager.SoundInstance.PlayEffectSound(_unsummonSound);

            }

            Destroy(_summonedCompanionCtrl.gameObject);
            _summonedCompanionCtrl = null;

        }

        private GameObject ReactivateCurrentSummon()
        {
            return _summonedCompanionCtrl.gameObject;
        }


        //IEnumerator TintAndDelayDestruction()
        //{
        //    float percentDone = 0f;
        //    Color redColor = Color.red;
        //    Color clearColor = Color.clear;

        //    //TODO: Loop until timer done
        //    _summonedCompanionCtrl.SetColor(Color.Lerp(clearColor, redColor, percentDone));


        //    Instantiate(_fireEffectPrefab, _summonedCompanionCtrl.gameObject.transform.position, this.transform.rotation);
        //    SoundManager.SoundInstance.PlayEffectSound(_rabbitSound);
        //    SoundManager.SoundInstance.PlayEffectSound(_fireSound);
        //    Destroy(_summonedCompanionCtrl.gameObject);
        //    _summonedCompanionCtrl = null;


        //}

        

    }
}