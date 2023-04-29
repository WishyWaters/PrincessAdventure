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
		BoomshroomWoods,
		LevelLoadTester,
		BoneTomb
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
		public bool hasFireball;
		public bool hasSummon;
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

		public ActiveGame()
        {
			gameScene = GameScenes.LevelLoadTester;
			loadLocationIndex = 0;
			gold = 0;
			keys = 0;
			heartPoints = 3;
			magicPoints = 3;
			luckPoints = 0;
			starShards = 0;
			hasMagic = false;
			hasFade = false;
			hasBomb = false;
			hasFireball = false;
			hasSummon = false;
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
			selectedFriend = 1;
			selectedHat = 0;
			selectedNecklace = 0;
			selectedOutfit = 0;
			selectedRing = 0;
			selectedShoes = 0;

		}

		public ActiveGame(GameDetails gameDetails)
		{
			gameScene = gameDetails.gameScene;
			loadLocationIndex = 0;
			gold = gameDetails.gold;
			keys = gameDetails.keys;
			heartPoints = gameDetails.heartPoints;
			magicPoints = gameDetails.magicPoints;
			luckPoints = gameDetails.luckPoints;
			starShards = gameDetails.starShards;
			hasMagic = gameDetails.hasMagic;
			hasFade = gameDetails.hasFade;
			hasBomb = gameDetails.hasBomb;
			hasFireball = gameDetails.hasFireball;
			hasSummon = gameDetails.hasSummon;
			hats = gameDetails.hats;
			outfits = gameDetails.outfits;
			shoes = gameDetails.shoes;
			necklaces = gameDetails.necklaces;
			rings = gameDetails.rings;
			friends = gameDetails.friends;
			selectedFriend = gameDetails.selectedFriend;
			selectedHat = gameDetails.selectedHat;
			selectedNecklace = gameDetails.selectedNecklace;
			selectedOutfit = gameDetails.selectedOutfit;
			selectedRing = gameDetails.selectedRing;
			selectedShoes = gameDetails.selectedShoes;

		}
	}

	public enum PowerUpOptions
	{
		Heart,
		Magic,
		Luck
	}

	public enum FadeTypes
    {
		Left,
		Right,
		Up,
		Down,
		Enter,
		Exit
    }
}
