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
        Soup,
        Apple


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

    public enum ScriptedMoveTypes
    {
        Loop,
        MoveThenDestroy,
        MoveThenIdle
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
    public class ItemDescription
    {
        public int id;
        public string name;
        public string text;
        public string extra;
    }

    [System.Serializable]
    public class ItemDescriptions
    {
        public List<ItemDescription> uniqueItems;
        public List<ItemDescription> friends;
        public List<ItemDescription> hats;
        public List<ItemDescription> necklaces;
        public List<ItemDescription> outfits;
        public List<ItemDescription> rings;
        public List<ItemDescription> shoes;
        public List<ItemDescription> trade;
    }

    [System.Serializable]
    public class QuestDescription
    {
        public int id;
        public int stage;
        public string title;
        public string text;
    }

    [System.Serializable]
    public class QuestDescriptions
    {
        public List<QuestDescription> descriptions;
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
