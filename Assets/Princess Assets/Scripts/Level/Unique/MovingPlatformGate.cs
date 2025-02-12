using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class MovingPlatformGate : MonoBehaviour
    {

        [SerializeField] private GameObject _keyTrigger;
        [SerializeField] private List<Collider2D> _gatesToOpen;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject == _keyTrigger)
            {
                foreach(Collider2D gate in _gatesToOpen)
                {
                    gate.enabled = false;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == _keyTrigger)
            {
                foreach (Collider2D gate in _gatesToOpen)
                {
                    gate.enabled = true;
                }
            }
        }
    }
}
