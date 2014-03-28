using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour
{
	public Font guiFont;
	public Texture2D boxImage;
	public Texture2D buttonImage;
	
	private Rect boxRect;
	private Rect buttonRect;
	private GUIStyle boxStyle;
	private GUIStyle buttonStyle;
	private GUIContent boxContent;
	private GUIContent buttonContent;
	private bool displayIntroBox;
	
	void Start ()
	{
		boxRect = new Rect (100, 100, Globals.originalWidth - 200, Globals.originalHeight - 200);
		buttonRect = new Rect (Globals.originalWidth/2 - 200, 700, 400, 200);
		
		boxStyle = new GUIStyle ();
		buttonStyle = new GUIStyle ();
		
		boxContent = new GUIContent ();
		buttonContent = new GUIContent ();
		
		displayIntroBox = true;
	}
	
	void OnGUI ()
	{
		// scale the gui to match the current screen size
		GUI.matrix = Globals.PrepareMatrix ();
		
		// prepare the style for the boxes
		boxStyle.font = guiFont;
		boxStyle.fontSize = 85;
		boxStyle.wordWrap = true;
		boxStyle.alignment = TextAnchor.UpperCenter;
		
		// prepare the style for buttons
		GUI.skin.button.normal.background = buttonImage;
		GUI.skin.button.hover.background = buttonImage;
		GUI.skin.button.active.background = buttonImage;
		
		buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.font = guiFont;
		buttonStyle.fontSize = 85;
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		
		// display the display boxes if neccessary
		drawIntroBox ();
		drawOutroBox ();
		
		// reset the resolution
		GUI.matrix = Matrix4x4.identity;
	}
	
	private void drawIntroBox ()
	{
		if (displayIntroBox)
		{
			//TODO drawBox (Globals.WashingtonFightIntros [(int)Globals.CurrentScene - 10], "Begin!");
			drawBox ("TODO DELETE ME     AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "Begin!");
			
			// create the start button
			if (GUI.Button (buttonRect, buttonContent, buttonStyle))
			{
				displayIntroBox = false;
				Globals.paused = false;
				GameTimer.StartTimer ();
				//TODO play button noise
			}
		}
	}
	
	private void drawOutroBox ()
	{
		if (Globals.GameState == BattleStateEnum.LOSE)
		{
			boxContent.text = "You Lost!";
		}
		else if (Globals.GameState == BattleStateEnum.TIE)
		{
			boxContent.text = "You Tied!";
		}
		else if (Globals.GameState == BattleStateEnum.WIN)
		{
			boxContent.text = "You Won!";
		}
		else
		{
			return;
		}
		
		//TODO drawBox (Globals.WashingtonFightOutros [(int)Globals.CurrentScene - 10], "Return to Map");
		drawBox ("TODO DELETE ME     AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "Return to Map");
		
		if (GUI.Button (buttonRect, buttonContent, buttonStyle))
		{
			Application.LoadLevel ("WashingtonMap");
			//TODO play button sound
			//AudioSource audio = gameObject.AddComponent<AudioSource> ();
			//audio.clip = Resources.Load ("Sounds/Menu Select Sound") as AudioClip;
		}	
	}
	
	private void drawBox (string boxText, string buttonText)
	{	
		// draw the box and text
		boxContent.text = boxText;
		GUI.DrawTexture (boxRect, boxImage, ScaleMode.StretchToFill);
		GUI.Label (new Rect(boxRect.x + 100, boxRect.y + 100, boxRect.width - 200, boxRect.height - 200), boxContent, boxStyle);
		
		// change the button text
		buttonContent.text = buttonText;
	}
}
