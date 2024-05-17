using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class MessageInteractionController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _title;
        [SerializeField] private AudioClip _Sfx;
        [SerializeField] private List<MessageModels.Message> _messages;



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowMessage()
        {
            //Get the right message based on conditions
            MessageModels.Message msgToShow = GetMessage();


            //Send msg to GUI
            GameManager.GameInstance.LoadMessageGui(msgToShow);
        }

        private MessageModels.Message GetMessage()
        {
            //always return the first valid
            foreach(MessageModels.Message msg in _messages)
            {
                if (msg.display == MessageModels.DisplayType.Heart)
                    continue;

                if (msg.conditions == null || msg.conditions.Count == 0)
                    return msg;

                if (msg.conditions[0].conditionType == MessageModels.MessageConditionTypes.None)
                    return msg;

                bool passedConditions = true;

                foreach(MessageModels.MessageCondition condition in msg.conditions)
                {
                    if (condition.conditionType == MessageModels.MessageConditionTypes.HasItem)
                        passedConditions = passedConditions && HasItem(condition.typeOrStage, condition.id);
                    else if(condition.conditionType == MessageModels.MessageConditionTypes.NotHasItem)
                        passedConditions = passedConditions && !HasItem(condition.typeOrStage, condition.id);
                    else if(condition.conditionType == MessageModels.MessageConditionTypes.QuestStatus)
                        passedConditions = passedConditions && QuestAtStage(condition.id, condition.typeOrStage);
                }

                if (passedConditions)
                    return msg;


            }


            return _messages[0];
        }

        private bool HasItem(int itemType, int itemId)
        {

            /*IDs for Items: 
            Friend 0
            Hat, 1
            Necklace, 2
            Outfit, 3
            Ring, 4
            Shoes 5
            Staff, 6
            Candle, 7
            Skull, 8
            Book, 9
            Gemstone, 10
            Soup 11
            */

            return GameManager.GameInstance.HasItem(itemType, itemId);
        }

        private bool QuestAtStage(int questId, int questStage)
        {
            //TODO: Check Quest stuff

            return false;
        }
    }
}
