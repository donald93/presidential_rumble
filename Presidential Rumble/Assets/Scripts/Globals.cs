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
		WashingtonFight1MP = 21,
		WashingtonFight2MP = 22,
		WashingtonFight3MP = 23,
		WashingtonFight4MP = 24
}
;

public enum BattleStateEnum
{
		WIN,
		LOSE,
		ONGOING,
};

public static class Globals
{
		public static readonly float originalWidth = 1920;
		public static readonly float originalHeight = 1080;
	
		public static bool paused = true, multiplayer = false;
		public static BattleStateEnum GameState = BattleStateEnum.ONGOING;
		public static SceneEnum CurrentScene;
	
		public static readonly string[][] WashingtonFightIntros = {
				new string[]{"1", "2"},
				new string[]{"a", "b"},
				new string[]{"1", "2", "3"},
				new string[]{"a", "b", "c", "d"},
				new string[]{"1", "2"}};
	
		public static readonly string[] WashingtonFightOutros = {
				"fighttut", 
				"The Battle of Jumonville Glen was a British Colonial victory that was a major contributing factor leading to the French and Indian War.  Joseph Coulon de Villiers de Jumonville was killed in battle, though the circumstances of his death are still up to debate.  Some believe that he was instead captured, the ordered killed by Washington himself.  After the victory, Washington retreated to Fort Necessity, where a nearby Canadian Force compelled his surrender.", 
				"The defeat of the English at Yorktown signified the end of the Revolutionary war.  General Cornwallis surrendered himself and his troops soon after the siege.  Later, the Treaty of Paris was signed.   Which saw the disbandment of the American army, the British evacuation of New York, and the resignation of Commander-in-Chief, George Washington.  A man who could have become the most powerful man in America instead chose to take up a life as a citizen in the country he helped create.", 
				"Benedict Arnold was a very popular icon in America during his times as a brigadier general.  He assaulted Quebec on December thirty-first, seventeen seventy-five, where his leg was shattered.  He played a vital role in delaying the British Northern invasion by slowing them at Saint-Jean and Valcour Island.  Where he was reported to be the last man to leave before the British arrived.  His betrayal came from his belief that the Revolutionary war was a lost cause, and that all men fighting were dead men.", 
				"(Washington’s own words regarding Colonel Nicola’s letter) “In no occurrence in the course of the war, has given me more painful sensations than your information of there being such ideas existing in the Army as you have expressed, and I must view with abhorrence, and reprehend with severity.  You could not have found a person whom your schemes are more disagreeable. If you have any regard for your Country, concern yourself or posterity, or respect for me, to banish these thoughts from your mind, and never communicate, as from yourself, or anyone else, a sentiment of the like nature.”"
		};
	
		public static Matrix4x4 PrepareMatrix ()
		{
				Vector2 ratio = new Vector2 (Screen.width / originalWidth, Screen.height / originalHeight);
				Matrix4x4 guiMatrix = Matrix4x4.identity;
				guiMatrix.SetTRS (new Vector3 (1, 1, 1), Quaternion.identity, new Vector3 (ratio.x, ratio.y, 1));
				return guiMatrix;
		}
}
