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
	public class PrincessStyle
	{
		public int bodyColor;
		public int hairStyle;
		public int hairColor;
		public int eyeStyle;
		public int eyeColor;
		public int glassesStyle;
		public int glassesColor;

		public PrincessStyle()
        {
			bodyColor = (int)PrincessSkinColor.color3;
			hairStyle = (int)PrincessHairStyles.Down;
			hairColor = (int)PrincessHairColor.color6;
			eyeStyle = (int)PrincessEyeShapes.Middle;
			eyeColor = (int)PrincessEyeColor.color3;
			glassesStyle = (int)PrincessGlassesStyle.None;
			glassesColor = (int)PrincessGlassesColor.color1;
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
            hats.Add(9);

            outfits = new List<int>();
            outfits.Add(1);
            outfits.Add(2);
            outfits.Add(3);
            outfits.Add(4);
            outfits.Add(5);
            outfits.Add(6);
            outfits.Add(7);
            outfits.Add(8);
            outfits.Add(9);

            shoes = new List<int>();
            shoes.Add(1);
            shoes.Add(2);
            shoes.Add(3);
            shoes.Add(4);
            shoes.Add(5);
            shoes.Add(6);
            shoes.Add(7);
            shoes.Add(8);
            shoes.Add(9);

            necklaces = new List<int>();
			rings = new List<int>();

            rings.Add(1);
            rings.Add(2);
            rings.Add(3);
            rings.Add(4);
            necklaces.Add(1);
            necklaces.Add(2);
            necklaces.Add(3);
            necklaces.Add(4);

            friends = new List<int>();
			friends.Add(1);
			//friends.Add(2);
			//friends.Add(3);

			selectedFriend = 1;
			selectedHat = 1;
			selectedNecklace = 0;
			selectedOutfit = 1;
			selectedRing = 0;
			selectedShoes = 1;

		}
	}

	[System.Serializable]
	public enum PrincessHairStyles
    {
		Bangs=0,
		Feathery=1,
		Down=2,
		Ponytail=3,
		DoubleBun=4,
		FaceFrame=5,
		TurnedOut=6,
		Mohawk=7,
		Spike=8,
		Floof=9,
		Slick=10,
		SlickPony=11,
		SlickBun=12,
		Pixie=13,
		Bald=14
    }

	[System.Serializable]
	public enum PrincessEyeShapes
    {
		Big=0,
		Middle=1,
		Narrow=2
    }

	[System.Serializable]
	public enum PrincessGlassesStyle
	{
		None=0,
		Round=1,
		Thick=2,
		Nose=3
	}

	[System.Serializable]
	public enum EquipSlots
    {
		Head=0,
		Body=1,
		Feet=2,
		Necklace=3,
		Ring=4,
		Friend=5
    }

	[System.Serializable]
	public enum EquipBonus
	{
		Hearts=0,
		Magic=1,
		Both=2,
		None=3
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

	[System.Serializable]
	public enum PrincessSkinColor
	{
		color1=0,
		color2=1,
		color3=2,
		color4=3,
		color5=4
	}

	[System.Serializable]
	public enum PrincessHairColor
	{
		color1 = 0,
		color2 = 1,
		color3 = 2,
		color4 = 3,
		color5 = 4,
		color6=5,
		color7=6,
		color8=7,
		color9=8,
		color10=9
	}

	[System.Serializable]
	public enum PrincessEyeColor
	{
		color1 = 0,
		color2 = 1,
		color3 = 2,
		color4 = 3,
		color5 = 4
	}

	[System.Serializable]
	public enum PrincessGlassesColor
    {
		color1 = 0,
		color2 = 1,
		color3 = 2,
		color4 = 3,
		color5 = 4
	}
}
