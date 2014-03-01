using UnityEngine;
using System.Collections;

/// <summary>
/// SceneEnum represents a level for each name.
/// This name must match the name of the scene 
/// in the Assets->Scenes folder.
/// 
/// For simplicity's sake, the int values are 
/// incremented by 10 for each important section.
/// </summary>
public enum SceneEnum
{
	MainMenu = 0,
	Main = 1, //TODO test scene only
	Fight2 = 2,
	MitchellsTextScene = 3,
	UITestScene = 4,
	
	//CharacterSelect = 1,
	//AboutDevelopers = 2,
	//...
	//Tutorial = 9,
	WashingtonMap = 10,
	WashingtonFight = 11
	//WashingtonLevel1 = 11,
	//WashingtonLevel2 = 12,
	
	// pres 2 map = 20
};

public enum Directions
{
	Up = 0,
	Down = 1,
	Left = 2,
	Right = 3
}

public class Globals
{
	public static readonly float originalWidth = 1920;
	public static readonly float originalHeight = 1080;
}
