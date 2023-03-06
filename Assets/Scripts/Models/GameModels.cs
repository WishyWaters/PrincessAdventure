using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
	Undefined,
	Loading,
	Playing,
	Cutscene,
	Menu
}

public enum GameScenes
{
	MainMenu,
	TestBox,
	StartMountain,

}

public enum ControllerTypes
{
	Xbox,
	Ps,
	Other,

}

[System.Serializable]
public class GameDetails
{

	public int saveID;
	public GameScenes gameScene;
	public int startPoint;
	public int gold;
	//TODO: Stats
	//TODO: Found Items
	//TODO: Current Equipment
	//TODO: Character attributes
	public bool hasMagic;
	public bool hasFade;
	public bool hasBomb;

}
