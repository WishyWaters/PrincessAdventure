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
		public int saveID;
		public GameScenes gameScene;
		public int loadLocationIndex;
		public int gold;
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
}
