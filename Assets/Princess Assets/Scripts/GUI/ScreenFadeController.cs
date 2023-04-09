using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrincessAdventure
{
    public class ScreenFadeController : MonoBehaviour
    {
        [SerializeField] private Image _background;
        // Start is called before the first frame update
        void Start()
        {
            _background.fillAmount = 0f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void BlackOut()
        {
            _background.fillAmount = 1f;
        }

        public void ClearOut()
        {
            _background.fillAmount = 0f;
        }

        public IEnumerator FillToBlack(FadeTypes type, float timeToTake)
        {
            float timeSoFar = 0f;
            ClearOut();

            switch (type)
            {
                case FadeTypes.EnterExit:
                    _background.fillAmount = 0f;
                    _background.fillMethod = Image.FillMethod.Radial360;
                    _background.fillClockwise = true;
                    _background.fillOrigin = (int)Image.Origin360.Bottom;
                    break;
                case FadeTypes.Left:
                    _background.fillAmount = 0f;
                    _background.fillMethod = Image.FillMethod.Horizontal;
                    _background.fillOrigin = (int)Image.OriginHorizontal.Left;
                    break;
                case FadeTypes.Right:
                    _background.fillAmount = 0f;
                    _background.fillMethod = Image.FillMethod.Horizontal;
                    _background.fillOrigin = (int)Image.OriginHorizontal.Right;
                    break;
                case FadeTypes.Up:
                    _background.fillAmount = 0f;
                    _background.fillMethod = Image.FillMethod.Vertical;
                    _background.fillOrigin = (int)Image.OriginVertical.Top;
                    break;
                case FadeTypes.Down:
                    _background.fillAmount = 0f;
                    _background.fillMethod = Image.FillMethod.Vertical;
                    _background.fillOrigin = (int)Image.OriginVertical.Bottom;
                    break;
            }

            float fillAmt = timeSoFar / timeToTake;
            while (timeSoFar <= timeToTake)
            {
                timeSoFar += Time.unscaledDeltaTime;
                fillAmt = timeSoFar / timeToTake;
                _background.fillAmount = fillAmt;

                yield return null;
            }

            BlackOut();
        }

        public IEnumerator FillToClear(FadeTypes type, float timeToTake)
        {
            float timeSoFar = timeToTake;
            BlackOut();

            switch (type)
            {
                case FadeTypes.EnterExit:
                    _background.fillAmount = 1f;
                    _background.fillMethod = Image.FillMethod.Radial360;
                    _background.fillClockwise = false;
                    _background.fillOrigin = (int)Image.Origin360.Bottom;
                    break;
                case FadeTypes.Left:
                    _background.fillAmount = 1f;
                    _background.fillMethod = Image.FillMethod.Horizontal;
                    _background.fillOrigin = (int)Image.OriginHorizontal.Right;
                    break;
                case FadeTypes.Right:
                    _background.fillAmount = 1f;
                    _background.fillMethod = Image.FillMethod.Horizontal;
                    _background.fillOrigin = (int)Image.OriginHorizontal.Left;
                    break;
                case FadeTypes.Up:
                    _background.fillAmount = 1f;
                    _background.fillMethod = Image.FillMethod.Vertical;
                    _background.fillOrigin = (int)Image.OriginVertical.Bottom;
                    break;
                case FadeTypes.Down:
                    _background.fillAmount = 1f;
                    _background.fillMethod = Image.FillMethod.Vertical;
                    _background.fillOrigin = (int)Image.OriginVertical.Top;
                    break;
            }

            float fillAmt = timeSoFar / timeToTake;
            while (timeSoFar > 0)
            {
                timeSoFar -= Time.unscaledDeltaTime;
                fillAmt = timeSoFar / timeToTake;
                _background.fillAmount = fillAmt;

                yield return null;
            }

            ClearOut();
        }
    }
}