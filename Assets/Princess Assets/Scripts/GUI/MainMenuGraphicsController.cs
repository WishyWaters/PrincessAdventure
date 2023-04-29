using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class MainMenuGraphicsController : MonoBehaviour
    {

        [SerializeField] RectTransform _top;
        [SerializeField] RectTransform _middle;
        [SerializeField] RectTransform _bottom;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _top.Rotate(new Vector3(0, 0, 1));
            _middle.Rotate(new Vector3(0, 0, -1));
            _bottom.Rotate(new Vector3(0, 0, 1));
        }
    }
}