using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomizableCharacters;

namespace PrincessAdventure
{
	public enum PrincessState
	{
		Neutral,
		Moving,
		//Run
		MagicCast,
		Summon,
		//Fade
		DropBomb,
		HoldingBomb,
		ThrowFireball,
		Falling,
		Disabled
	}

	public enum FaceTypes
    {
		Default,
		Angry,
		Smile,
		Surprised,
		Sleep

    }

	public enum CompanionState
    {
		Neutral,
		Moving,
		Casting,
		Disabled
    }

	public enum CompanionPower
    {
		Slime,
		Bat,
		Bunny
    }

	public class PrincessInputActions
	{
		public Vector2 MoveAxis { get; set; }
		public bool InputRunning { get; set; }
		public bool InputInteract { get; set; }

		public bool InputMagicCast { get; set; }
		public bool InputSummoning { get; set; }
		public bool InputSummonComplete { get; set; }

		public bool InputFade { get; set; }

		public bool InputDropBomb { get; set; }
		public bool InputHoldBomb { get; set; }
		public bool InputThrowFireball { get; set; }
	}

	[System.Serializable]
	public class PrincessCustomizations
    {
		public Color BodyColor { get; set; }
		public PrincessHairStyles HairStyle { get; set; }
		public Color HairColor { get; set; }
		public PrincessEyeShapes EyeStyle { get; set; }
		public Color EyeColor { get; set; }
		public PrincessGlassesStyle GlassesStyle { get; set; }
		public Color GlassesColor { get; set; }

		public PrincessCustomizations()
        {
			//BodyColor = Color.white;

        }
	}

	[System.Serializable]
	public class PrincessEquipment
	{
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

		public PrincessEquipment()
        {
			hats = new List<int>();
			hats.Add(1);
			hats.Add(2);
			hats.Add(3);
			hats.Add(4);
			hats.Add(5);
			hats.Add(6);
			hats.Add(7);
			hats.Add(8);
			//hats.Add(9);

			outfits = new List<int>();
			outfits.Add(1);
			outfits.Add(2);
			outfits.Add(3);
			outfits.Add(4);
			outfits.Add(5);
			outfits.Add(6);
			outfits.Add(7);
			outfits.Add(8);
			//outfits.Add(9);

			shoes = new List<int>();
			shoes.Add(1);
			shoes.Add(2);
			shoes.Add(3);
			shoes.Add(4);
			shoes.Add(5);
			shoes.Add(6);
			shoes.Add(7);
			shoes.Add(8);
			//shoes.Add(9);

			necklaces = new List<int>();
			rings = new List<int>();
			friends = new List<int>();
			friends.Add(1);

			selectedFriend = 1;
			selectedHat = 1;
			selectedNecklace = 0;
			selectedOutfit = 1;
			selectedRing = 0;
			selectedShoes = 1;

		}
	}

	public enum PrincessHairStyles
    {
		Bangs,
		Feathery,
		Down,
		Ponytail,
		DoubleBun,
		FaceFrame,
		TurnedOut,
		Mohawk,
		Spike,
		Floof,
		Slick,
		SlickPony,
		SlickBun,
		Pixie,
		Bald
    }

	public enum PrincessEyeShapes
    {
		Big,
		Middle,
		Narrow
    }

	public enum PrincessGlassesStyle
	{
		None,
		Round,
		Thick,
		Nose
	}

	public enum EquipSlots
    {
		Head,
		Body,
		Feet,
		Necklace,
		Ring,
		Friend
    }

	public enum EquipBonus
	{
		Hearts,
		Magic,
		Both,
		None
	}

	[System.Serializable]
	public class HatEquip
    {
		public CustomizationData hat;
		public Color color;
		public bool requiresHatHair;
		public bool hasDetail;
		public int detailIndex;
		public Color detailColor;
	}

	[System.Serializable]
	public class ShoeEquip
	{
		public CustomizationData shoe;
		public Color color;
		public bool hasDetail;
		public int detailIndex;
		public Color detailColor;
	}

	[System.Serializable]
	public class OutfitEquip
	{
		public CustomizationData top;
		public Color topColor1;
		public Color topColor2;
		public bool hasTopDetail;
		public int topDetailIndex;
		public CustomizationData topOverlay;
		public Color topOverColor1;
		public Color topOverColor2;
		public CustomizationData belt;
		public Color beltColor1;
		public Color beltColor2;
		public bool hasBeltDetail;
		public int beltDetailIndex;
		public CustomizationData pants;
		public Color pantColor1;
		public Color pantColor2;
		public CustomizationData gloves;
		public Color gloveColor1;
		public Color gloveColor2;
		public CustomizationData shoulder;
		public Color shoulderColor1;
		public Color shoulderColor2;
	}
}
