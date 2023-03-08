using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
	public enum CharacterState
	{
		Neutral,
		Moving,
		//Run
		MagicCast,
		Summon,
		//Fade
		DropBomb,
		HoldingBomb,
		ThrowBomb
	}

	public class PrincessInputActions
	{
		public Vector2 MoveAxis { get; set; }
		public bool InputRunning { get; set; }
		public bool InputInteract { get; set; }

		public bool InputMagicCast { get; set; }
		public bool InputSummon { get; set; }

		public bool InputFade { get; set; }

		public bool InputDropBomb { get; set; }
		public bool InputHoldBomb { get; set; }
		public bool InputThrowBomb { get; set; }
	}

	
}
