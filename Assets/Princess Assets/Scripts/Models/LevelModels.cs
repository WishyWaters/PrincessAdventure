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
        MajorChest

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
        Patrol,
        Follow,
        Attack,
        Disabled
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
        Horde
        
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
}
