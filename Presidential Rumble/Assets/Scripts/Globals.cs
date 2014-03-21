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
	//TODO test scenes only
	Main = 1,
	Fight2 = 2,
	WashingtonFight = 3,
	//TODO
	WashingtonMap = 10,
	WashingtonTutorial = 11,
	WashingtonFight1 = 12,
	WashingtonFight2 = 13,
	WashingtonFight3 = 14,
	WashingtonFight4 = 15,
};

public enum Directions
{
	Up = 0,
	Down = 1,
	Left = 2,
	Right = 3
}

public static class Globals
{
	public static readonly float originalWidth = 1920;
	public static readonly float originalHeight = 1080;

	public static Matrix4x4 PrepareMatrix()
	{
		Vector2 ratio = new Vector2(Screen.width/originalWidth , Screen.height/originalHeight );
		Matrix4x4 guiMatrix = Matrix4x4.identity;
		guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
		return guiMatrix;
	}
}
