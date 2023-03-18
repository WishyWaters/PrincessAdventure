using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{

	public enum GameState
	{
		Undefined,
		Loading,
		Playing,
		Cutscene,
		Menu
	}

	public enum GameScenes
	{
		MainMenu,
		TestBox,
		StartMountain,

	}

	public enum ControllerTypes
	{
		Xbox,
		Ps,
		Other,

	}

	[System.Serializable]
	public class GameDetails
	{
		public int saveId;
		public GameScenes gameScene;
		public int loadLocationIndex;
		public int gold;
		public int keys;
		public int maxHearts;
		public int maxMana;
		public int starShards;
		public bool hasMagic;
		public bool hasFade;
		public bool hasBomb;
		public List<int> hats;
		public List<int> outfits;
		public List<int> shoes;
		public List<int> necklaces;
		public List<int> rings;
		public List<int> friends;

	}

	[System.Serializable]
	public class ActiveGame : GameDetails
    {
		public int currentHealth;
		public int currentManaPoints;
		public int totalManaPoints;

		public ActiveGame(int newSaveId)
        {
			saveId = newSaveId;
			gameScene = GameScenes.StartMountain;
			loadLocationIndex = 1;
			gold = 0;
			maxHearts = 3;
			maxMana = 3;
			starShards = 0;
			hasMagic = false;
			hasFade = false;
			hasBomb = false;
			hats = new List<int>();
			outfits = new List<int>();
			shoes = new List<int>();
			necklaces = new List<int>();
			rings = new List<int>();
			friends = new List<int>();

		}
	}
}
