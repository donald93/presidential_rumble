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
		"May Twenty-Eighth, Seventeen Fifty-Four\n\nThe Battle of Jumonville Glen\nTensions between our British colonies and the French are rising.  A large Canadian force has recently driven off a construction team we sent in and plan to threaten us further.  I have aligned myself with an Iroquis who goes by the name of Tanacharison, or “Half-King”.  With the extra support from the natives, I plan on ambushing the Canadian camp and capturing their commander, Joseph Coulon de Villiers de Jumonville, and ending the conflict in this region.", 
		"October Nineteenth, Seventeen Eighty-One\n\nSiege of Yorktown\nThis is it, this is fight that will end our war with the British and earn us our freedom.  But first, we have to make it through General Cornwallis and his nine-thousand troops.  I have no doubt that our American army combined with our French allies will break their wills force a surrender.  We will march forward with bravery in our hearts and freedom on our minds, the American people will be given their rights at last. ", 
		"September Fifteenth, Seventeen Eighty\n\nThe Betrayal of Benedict Arnold\nBenedict Arnold, a friend of mine, a trusted general and leader of the American people has left us for his English Masters.  He was in command of the fortifications of West Point, and the man planned on surrendering it to the British! We foiled his plans, but he has still evaded capture and is now a brigadier general of the British forces.  He has to pay for his actions, justice will come to this man.", 
		"Sleepless night after the battle of Yorktown\n\nThe Man Who Would Not Be King\nI believe all men have natural rights, rights that can’t be stepped on or taken away by any single man.  And the role of this new government is to protect those rights.  That role is quickly being questioned.  Congress has delayed the payment to our soldiers for too long.  A young Colonel has recently written me on the ineffectiveness of congress, and has asked if I would step in as King of America.  Kings are exactly what we fought to protect ourselves against, yet it would make it easier for me to get the needed resources to men who were willing to give their lives for this country…"};
	
		public static readonly string[] WashingtonFightOutros = {"fighttut", 
		"The Battle of Jumonville Glen was a British Colonial victory that was a major contributing factor leading to the French and Indian War.  Joseph Coulon de Villiers de Jumonville was killed in battle, though the circumstances of his death are still up to debate.  Some believe that he was instead captured, the ordered killed by Washington himself.  After the victory, Washington retreated to Fort Necessity, where a nearby Canadian Force compelled his surrender.", 
		"The defeat of the English at Yorktown signified the end of the Revolutionary war.  General Cornwallis surrendered himself and his troops soon after the siege.  Later, the Treaty of Paris was signed.   Which saw the disbandment of the American army, the British evacuation of New York, and the resignation of Commander-in-Chief, George Washington.  A man who could have become the most powerful man in America instead chose to take up a life as a citizen in the country he helped create.", 
		"Benedict Arnold was a very popular icon in America during his times as a brigadier general.  He assaulted Quebec on December thirty-first, seventeen seventy-five, where his leg was shattered.  He played a vital role in delaying the British Northern invasion by slowing them at Saint-Jean and Valcour Island.  Where he was reported to be the last man to leave before the British arrived.  His betrayal came from his belief that the Revolutionary war was a lost cause, and that all men fighting were dead men.", 
		"(Washington’s own words regarding Colonel Nicola’s letter) “In no occurrence in the course of the war, has given me more painful sensations than your information of there being such ideas existing in the Army as you have expressed, and I must view with abhorrence, and reprehend with severity.  You could not have found a person whom your schemes are more disagreeable. If you have any regard for your Country, concern yourself or posterity, or respect for me, to banish these thoughts from your mind, and never communicate, as from yourself, or anyone else, a sentiment of the like nature.”"};
	
		public static Matrix4x4 PrepareMatrix ()
		{
				Vector2 ratio = new Vector2 (Screen.width / originalWidth, Screen.height / originalHeight);
				Matrix4x4 guiMatrix = Matrix4x4.identity;
				guiMatrix.SetTRS (new Vector3 (1, 1, 1), Quaternion.identity, new Vector3 (ratio.x, ratio.y, 1));
				return guiMatrix;
		}
}
