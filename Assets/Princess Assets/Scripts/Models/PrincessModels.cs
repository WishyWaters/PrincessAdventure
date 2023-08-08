using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

			outfits = new List<int>();
			outfits.Add(1);

			shoes = new List<int>();
			shoes.Add(1);

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

}
