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
		public int starShards;
		public bool hasMagic;
		public bool hasFade;
		public bool hasBomb;
		public bool hasFireball;
		public bool hasSummon;
		public bool hasVision;
		public PrincessEquipment equipment;
		public PrincessCustomizations customizations;

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
			starShards = 0;
			hasMagic = false;
			hasFade = false;
			hasBomb = false;
			hasFireball = false;
			hasSummon = false;

		}

		public ActiveGame(GameDetails gameDetails)
		{
			gameScene = gameDetails.gameScene;
			loadLocationIndex = 0;
			gold = gameDetails.gold;
			keys = gameDetails.keys;
			heartPoints = gameDetails.heartPoints;
			magicPoints = gameDetails.magicPoints;
			starShards = gameDetails.starShards;
			hasMagic = gameDetails.hasMagic;
			hasFade = gameDetails.hasFade;
			hasBomb = gameDetails.hasBomb;
			hasFireball = gameDetails.hasFireball;
			hasSummon = gameDetails.hasSummon;
			equipment = new PrincessEquipment();
			customizations = new PrincessCustomizations();
		}
	}

	public enum PowerUpOptions
	{
		Heart,
		Magic
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


	[System.Serializable]
	public class EquipInformation
	{
		public int id;
		public EquipSlots slot;
		public Color color;
		public Sprite icon;
		public EquipBonus bonusStats;
	}

}
