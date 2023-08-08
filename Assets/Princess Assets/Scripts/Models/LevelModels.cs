using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
	public enum InteractionTypes
    {
        Jump,
        MinorChest,
        Talk,
        Door,
        MajorChest,
        Lever,
        Message

    }

    public enum AffectedBehavior
    {
        OneTimeResetOnLoad,
        OneTimeSave,
        Toggle,
        TimedReset,
        ToggleResetOnLoad

    }

    public class Interactable
    {
        public int interactableId;
        public InteractionTypes type;
        public int relatedId;
        public Vector2 requiredDirection;
    }

    public enum PickUps
    {
        RedPotion,
        BluePotion,
        GreenPotion,
        Key,
        Coin,
        SilverBar,
        GoldBar,
        StarShard,
        Staff,
        Candle,
        Skull,
        Book,
        Gemstone,
        Soup


    }

    public enum EnemyStates
    {
        Patrolling,
        Idle,
        Attack,
        Hurt,
        Disabled,
        Fleeing
    }

    public enum TreasureDrops
    {
        MinCoins,
        MedCoins,
        LotCoins,
        SinglePotion,
        ManyPotions,
        MinAll,
        MedAll,
        LotAll,
        Horde,
        PoisonPotion
        
    }

    public enum MajorTreasures
    {
        StarShard,
        Friend,
        Hat,
        Necklace,
        Outfit,
        Ring,
        Shoes
    }

    public enum EnemyRigTypes
    {
        CustomHumanoid,
        SpineMonster
    }

    public enum EnemyPatrolTypes
    {
        random,
        backForth,
        sharpTurn
    }

    [System.Serializable]
    public class MessageData
    {
        public int id;
        public string message;
    }

    [System.Serializable]
    public class LevelMessages
    {
        public List<MessageData> messages;
    }

    [System.Serializable]
    public class ToggleSave
    {
        public int id;
        public bool wasToggled;

        public ToggleSave(int newId, bool value)
        {
            id = newId;
            wasToggled = value;
        }
    }

    [System.Serializable]
    public class ReferenceSave
    {
        public int id;
        public int referenceId;

        public ReferenceSave(int newId, int value)
        {
            id = newId;
            referenceId = value;
        }
    }

    [System.Serializable]
    public class LevelSave
    {
        public List<ToggleSave> toggles;
        public List<ReferenceSave> references;

        public LevelSave()
        {
            toggles = new List<ToggleSave>();
            references = new List<ReferenceSave>();
        }

    }




}
