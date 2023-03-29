using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class SparkleController : MonoBehaviour
    {
        [SerializeField] private GameObject _sparkleEffectPrefab;
        [SerializeField] private AudioClip _sparkleSound;
        [SerializeField] private LayerMask _layersToPickup;

        private GameObject _sparkleEffect;

        public void HandleSparkleCast(Vector2 direction)
        {
            _sparkleEffect = Instantiate(_sparkleEffectPrefab, this.transform);

            //Move & Rotate to match direction
            SetEffectPosition(direction);


            SparkleReflectController reflectCtrl = _sparkleEffect.GetComponent<SparkleReflectController>();
            reflectCtrl.HandleSparkleReflect(direction);

            if (_sparkleSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_sparkleSound);

            PickupGrab();
        }

        private void PickupGrab()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)_sparkleEffect.transform.position, 1.2f, _layersToPickup);

            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Pickup")
                {
                    PickupController pickupCtrl = collider.gameObject.GetComponent<PickupController>();
                    pickupCtrl.PickupItem();
                }
            }
        }

        private void SetEffectPosition(Vector2 direction)
        {
            if (direction == Vector2.down)
            {
                _sparkleEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
                _sparkleEffect.transform.localPosition = new Vector3(0, 0, 0);
            }
            else if (direction == Vector2.right)
            {
                _sparkleEffect.transform.localRotation = Quaternion.Euler(0, 0, 90);
                _sparkleEffect.transform.localPosition = new Vector3(.5f, .2f, 0);
            }
            else if (direction == Vector2.up)
            {
                _sparkleEffect.transform.localRotation = Quaternion.Euler(0, 0, 180);
                _sparkleEffect.transform.localPosition = new Vector3(0, .7f, 0);
            }
            else if (direction == Vector2.left)
            {
                _sparkleEffect.transform.localRotation = Quaternion.Euler(0, 0, 270);
                _sparkleEffect.transform.localPosition = new Vector3(-.5f, .5f, 0);
            }
        }

    }
}
