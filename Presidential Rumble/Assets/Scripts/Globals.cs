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
	WashingtonMap = 9,
	WashingtonTutorial = 10,
	WashingtonFight1 = 11,
	WashingtonFight2 = 12,
	WashingtonFight3 = 13,
	WashingtonFight4 = 14,
};

public enum BattleStateEnum
{
	WIN,
	LOSE,
	TIE,
	ONGOING,
};

public static class Globals
{
	public static readonly float originalWidth = 1920;
	public static readonly float originalHeight = 1080;
	
	public static bool paused = true;
	public static BattleStateEnum GameState = BattleStateEnum.ONGOING;
	public static SceneEnum CurrentScene;
	
	public static readonly string[] WashingtonFightIntros = {"fighttut", 
		"fight1", 
		"fight2", 
		"fight3", 
		"fight4"};
	
	public static readonly string[] WashingtonFightOutros = {"fighttut", 
		"fight1", 
		"fight2", 
		"fight3", 
		"fight4"};
	
	public static Matrix4x4 PrepareMatrix ()
	{
		Vector2 ratio = new Vector2 (Screen.width / originalWidth, Screen.height / originalHeight);
		Matrix4x4 guiMatrix = Matrix4x4.identity;
		guiMatrix.SetTRS (new Vector3 (1, 1, 1), Quaternion.identity, new Vector3 (ratio.x, ratio.y, 1));
		return guiMatrix;
	}
}
