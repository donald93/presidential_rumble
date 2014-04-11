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
				new string[]{"Tutorial", "Second page of Tutorial"},
				new string[]{"\nMay Twenty-Eighth, Seventeen Fifty-Four\n\nThe Battle of Jumonville Glen", "\nLieutenant George Washington hears of a French unit camped out by the Ohio River.", "\nAllying himself with native Mingo warriors, Washington nears the camp with fifty-two men.", "\nWhen night arrives, Washington ambushes the sleeping French troops, who are under the command of Joseph Coulon de Villiers de Jumonville.", "\nNow get ready to RUMBLE!!!"},
				new string[]{"\nSeptember Fifteenth, Seventeen Eighty\n\nThe Betrayal of Benedict Arnold", "\nBenedict Arnold began as a skilled and heroic leader of the American people.", "\nAfter his unsuccessful attack of Quebec, Arnold watched as lower-ranked men were promoted faster than himself.", "\nTacked along with a debt his second wife was accumulating in Philadelphia, Arnold built up a resentment to his country.", "\nIn seventeen eighty, Arnold attempted to hand over his command of West Point fort to British forces.", "\nNow get ready to RUMBLE!!!"},
				new string[]{"\nSeptember Twenty-Sixth, Seventeen Eight-One\n\nSiege of Yorktown", "\nWashington, now a General in the Continental Army, laid siege to Yorktown with his French allies.", "\nEighteen thousand men surrounded the British defenses in New York City and held their ground while artillery bombarded English soldiers.", "\nAfter three days of bombarding the English troops, Washington marched closer to Yorktown and closer to the British commander General Cornwallis.", "\nNow get ready to RUMBLE!!!" },
				new string[]{"\n\nThe Man Who Would Not Be King", "\nSome want me to be king...", "\nI could be king...", "\nI must resist...", "\nNow get ready to RUMBLE!!!"}};
	
		public static readonly string[] WashingtonFightOutros = {
				"fighttut", 
				"The Battle of Jumonville Glen was the first British victory in the French and Indian War.  With a single man killed under Washington’s command, it was a huge success for our young soon to be President.  Joseph Coulon de Villiers de Jumonville was killed in the ambush, though his death is still up to debate.  After the victory, Washington retreated to Fort Necessity, where a nearby Canadian Force compelled his surrender.", 
				"Benedict Arnold had great potential to become just as famous as George Washington, but through a mix of bad luck and certain enemies, he sealed his fate as a traitor.  His “Sale” of West Point to the British was soon foiled, and Arnold died in relative obscurity in eighteen o’ one while in his London home.", 
				"The defeat of the English at Yorktown signified the end of the Revolutionary war.  General Cornwallis surrendered himself and his troops soon after the siege.  Later, the Treaty of Paris was signed.   Which saw the disbandment of the American army, the British evacuation of New York, and the resignation of Commander-in-Chief, George Washington.  A man who could have become the most powerful man in America instead chose to take up a life as a citizen in the country he helped create.",
				"“In no occurrence in the course of the war, has given me more painful sensations than your information of there being such ideas existing in the Army as you have expressed, and I must view with abhorrence, and reprehend with severity.  You could not have found a person whom your schemes are more disagreeable. If you have any regard for your Country, concern yourself or posterity, or respect for me, to banish these thoughts from your mind, and never communicate, as from yourself, or anyone else, a sentiment of the like nature.”"
		};
	
		public static Matrix4x4 PrepareMatrix ()
		{
				Vector2 ratio = new Vector2 (Screen.width / originalWidth, Screen.height / originalHeight);
				Matrix4x4 guiMatrix = Matrix4x4.identity;
				guiMatrix.SetTRS (new Vector3 (1, 1, 1), Quaternion.identity, new Vector3 (ratio.x, ratio.y, 1));
				return guiMatrix;
		}
}
