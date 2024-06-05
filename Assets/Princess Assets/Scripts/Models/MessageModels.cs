using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PrincessAdventure
{
	public class MessageModels : MonoBehaviour
	{
		[System.Serializable]
		public enum MessageConditionTypes
		{
			None = 0,
			HasItem = 1,
			NotHasItem = 2,
			ToggleStatus = 3
		}

		[System.Serializable]
		public enum DisplayType
		{
			Dialog = 0,
			Sign = 1,
			Tip = 2,
			Heart = 3
		}

		[System.Serializable]
		public enum ResponseTypes
		{
			Ok = 0,
			Shop = 1,
			GiveItem = 2,
			UpdateQuest = 3
		}

		[System.Serializable]
		public class MessageCondition
        {
			public MessageConditionTypes conditionType;
			public int id;
			public int typeOrStage;
        }

		[System.Serializable]
		public class MessageResponse
		{
			public ResponseTypes responseType;
			public int id;
			public int typeOrStage;
		}

		[System.Serializable]
		public class Message
		{
			public List<int> messageIds;
			public DisplayType display;
			public List<MessageCondition> conditions;
			public MessageResponse response;

		}

	}
}
