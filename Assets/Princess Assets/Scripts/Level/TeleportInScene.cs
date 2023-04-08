using UnityEngine;
using System.Collections;

namespace PrincessAdventure
{
    public class TeleportInScene : MonoBehaviour
    {
        [SerializeField] Transform _targetLocation;


        void OnTriggerEnter2D(Collider2D col)
        {
            //Debug.Log("item enter " + col.name);
            if (col.tag == "Player" || col.tag == "Companion")
            {
                GameManager.GameInstance.TeleportPlayerWithinScene(_targetLocation.position);
            }

        }




    }
}