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
        TimedReset

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
        StarShard
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
    public class SceneMessages
    {
        public List<MessageData> messages;
    }

    [System.Serializable]
    public class OneTimeObjects
    {
        int id;
        public bool wasUsed;
    }
}
