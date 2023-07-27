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

	public class PrincessCustomizations
    {
		public Color BodyColor { get; set; }
		public PrincessHairStyles HairStyle { get; set; }
		public Color HairColor { get; set; }
		public PrincessEyeShapes EyeStyle { get; set; }
		public Color EyeColor { get; set; }
		public PrincessGlassesStyle GlassesStyle { get; set; }
		public Color GlassesColor { get; set; }

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

	
}
