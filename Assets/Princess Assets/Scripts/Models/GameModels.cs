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
		Menu,
		Defeated
	}

	public enum GameScenes
	{
		MainMenu,
		TestBox,
		BoomshroomWoods,
		LevelLoadTester,
		BoneTomb,
		CharCreator,
		MirrorMountain
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
		public PrincessEquipment equipment = new();
		public PrincessStyle princessStyle = new();


	}

	[System.Serializable]
	public class ActiveGame : GameDetails
	{
		public int currentHealth;
		public int currentManaPoints;
		public int maxManaPoints { get; private set; }
		public int maxHealth { get; private set; }
		public int maxMagic { get; private set; }


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
			equipment = gameDetails.equipment;
			princessStyle = gameDetails.princessStyle;
		}

		public void RecalculateMaxStats()
        {
			int tempHealth = 0;
			int tempMana = 0;

			if (equipment.selectedHat == 2 || equipment.selectedHat == 3 || equipment.selectedHat == 6)
				tempHealth += 2;
			else if (equipment.selectedHat == 5 || equipment.selectedHat == 8)
				tempMana += 2;
			else if (equipment.selectedHat != 1)
			{
				tempHealth++;
				tempMana++;
            }

			if (equipment.selectedOutfit == 2 || equipment.selectedOutfit == 6)
				tempHealth += 2;
			else if (equipment.selectedOutfit == 5 || equipment.selectedOutfit == 8)
				tempMana += 2;
			else if (equipment.selectedOutfit != 1)
			{
				tempHealth++;
				tempMana++;
			}

			if (equipment.selectedShoes == 2 || equipment.selectedShoes == 6)
				tempHealth += 2;
			else if (equipment.selectedShoes == 3 || equipment.selectedShoes == 5 || equipment.selectedShoes == 8)
				tempMana += 2;
			else if (equipment.selectedShoes != 1)
			{
				tempHealth++;
				tempMana++;
			}

			if (equipment.selectedNecklace == 3 )
				tempHealth += 2;
			else if (equipment.selectedNecklace == 4)
				tempMana += 2;
			else if (equipment.selectedNecklace != 0)
			{
				tempHealth++;
				tempMana++;
			}

			if (equipment.selectedRing == 3)
				tempHealth += 2;
			else if (equipment.selectedRing == 4)
				tempMana += 2;
			else if (equipment.selectedRing != 0)
			{
				tempHealth++;
				tempMana++;
			}

			tempHealth += heartPoints;
			tempMana += magicPoints;

			maxMagic = tempMana;
			maxHealth = tempHealth;
			maxManaPoints = tempMana * 50;

			if (currentHealth > maxHealth)
				currentHealth = maxHealth;
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
