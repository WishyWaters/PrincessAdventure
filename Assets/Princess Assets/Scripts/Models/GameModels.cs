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
		BoomshroomForest
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
		public int heartPoints;
		public int magicPoints;
		public int luckPoints;
		public int starShards;
		public bool hasMagic;
		public bool hasFade;
		public bool hasBomb;
		public List<int> hats;
		public int selectedHat;
		public List<int> outfits;
		public int selectedOutfit;
		public List<int> shoes;
		public int selectedShoes;
		public List<int> necklaces;
		public int selectedNecklace;
		public List<int> rings;
		public int selectedRing;
		public List<int> friends;
		public int selectedFriend;


	}

	[System.Serializable]
	public class ActiveGame : GameDetails
    {
		public int currentHealth;
		public int currentManaPoints;
		public int maxManaPoints;

		public ActiveGame(int newSaveId)
        {
			saveId = newSaveId;
			gameScene = GameScenes.StartMountain;
			loadLocationIndex = 1;
			gold = 0;
			heartPoints = 3;
			magicPoints = 3;
			luckPoints = 3;
			starShards = 0;
			hasMagic = false;
			hasFade = false;
			hasBomb = false;
			hats = new List<int>();
			hats.Add(0);
			outfits = new List<int>();
			outfits.Add(0);
			shoes = new List<int>();
			shoes.Add(0);
			necklaces = new List<int>();
			necklaces.Add(0);
			rings = new List<int>();
			rings.Add(0);
			friends = new List<int>();
			friends.Add(0);
			selectedFriend = 0;
			selectedHat = 0;
			selectedNecklace = 0;
			selectedOutfit = 0;
			selectedRing = 0;
			selectedShoes = 0;

		}
	}

	public enum PowerUpOptions
	{
		Heart,
		Magic,
		Luck
	}
}
