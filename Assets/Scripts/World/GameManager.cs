using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager _GameManagerInstance;
		private GameState currentState = GameState.Undefined;

		private void Awake()
		{
			if (_GameManagerInstance == null)
			{
				DontDestroyOnLoad(this);
				_GameManagerInstance = this;
			}
			else if (_GameManagerInstance != this)
			{
				Destroy(gameObject);
			}
		}

	}
}
