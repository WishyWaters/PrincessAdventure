using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class ThrowFireballController : MonoBehaviour
    {
        [SerializeField] private GameObject _fireballPrefab;
        [SerializeField] private GameObject _throwEffectPrefab;
        [SerializeField] private AudioClip _throwSound;

        private GameObject _throwEffect;
        private GameObject _fireball;

        public void ThrowFireball(Vector2 direction)
        {
            if (_throwEffect == null)
                _throwEffect = Instantiate(_throwEffectPrefab, this.transform);

            _fireball = Instantiate(_fireballPrefab, this.transform.position, this.transform.rotation);

            SetEffectPosition(direction, this.transform.position);

            ParticleSystem partSys = _throwEffect.GetComponent<ParticleSystem>();
            if (direction != Vector2.up && _throwEffect != null && partSys != null)
                partSys.Play();

            if (_throwSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_throwSound);

            ProjectileController fireballCtrl = _fireball.GetComponent<ProjectileController>();
            if (fireballCtrl != null)
                fireballCtrl.InitializeProjectile(direction);

        }

        private void SetEffectPosition(Vector2 direction, Vector3 worldPosition)
        {
            if (direction == Vector2.down)
            {
                _throwEffect.transform.localRotation = Quaternion.Euler(0, 0, 90);
                _throwEffect.transform.localPosition = new Vector3(0, -.4f, 0);

                _fireball.transform.localRotation = Quaternion.Euler(0, 0, 90);
                _fireball.transform.localPosition = worldPosition + new Vector3(0, -.75f, 0);
            }
            else if (direction == Vector2.right)
            {
                _throwEffect.transform.localRotation = Quaternion.Euler(0, 0, 180);
                _throwEffect.transform.localPosition = new Vector3(1.4f, .65f, 0);

                _fireball.transform.localRotation = Quaternion.Euler(0, 0, 180);
                _fireball.transform.localPosition = worldPosition + new Vector3(1.5f, .75f, 0);
            }
            else if (direction == Vector2.up)
            {
                _throwEffect.transform.localRotation = Quaternion.Euler(0, 0, 270);
                _throwEffect.transform.localPosition = new Vector3(0, 2f, 0);

                _fireball.transform.localRotation = Quaternion.Euler(0, 0, 270);
                _fireball.transform.localPosition = worldPosition + new Vector3(0, 2f, 0);
            }
            else if (direction == Vector2.left)
            {
                _throwEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
                _throwEffect.transform.localPosition = new Vector3(-1.45f, .8f, 0);

                _fireball.transform.localRotation = Quaternion.Euler(0, 0, 0);
                _fireball.transform.localPosition = worldPosition + new Vector3(-1.5f, .75f, 0);
            }
        }
    }
}
