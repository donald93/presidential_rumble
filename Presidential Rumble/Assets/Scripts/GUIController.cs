using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour
{
		public Font guiFont;
		public Texture2D boxImage;
		public Texture2D buttonImage;
		public SceneEnum levelCode;
	
		private Rect boxRect;
		private Rect buttonRect;
		private GUIStyle boxStyle;
		private GUIStyle buttonStyle;
		private GUIContent boxContent;
		private GUIContent buttonContent;
		private bool displayIntroBox;
		private bool displayOutroBox;
		private int currentIntro;
	
		void Start ()
		{
				boxRect = new Rect (100, 100, Globals.originalWidth - 200, Globals.originalHeight - 200);
				buttonRect = new Rect (Globals.originalWidth / 2 - 200, 700, 400, 200);
		
				boxStyle = new GUIStyle ();
				buttonStyle = new GUIStyle ();
		
				boxContent = new GUIContent ();
				buttonContent = new GUIContent ();
		
				displayIntroBox = true;
				displayOutroBox = false;

				Globals.CurrentScene = levelCode;
				Globals.GameState = BattleStateEnum.ONGOING;

				currentIntro = 0;
		}
	
		void OnGUI ()
		{
				// scale the gui to match the current screen size
				GUI.matrix = Globals.PrepareMatrix ();
		
				// prepare the style for the boxes
				boxStyle.font = guiFont;
				boxStyle.fontSize = 45;
				boxStyle.wordWrap = true;
				boxStyle.alignment = TextAnchor.UpperCenter;
	
				// prepare the style for buttons
				GUI.skin.button.normal.background = buttonImage;
				GUI.skin.button.hover.background = buttonImage;
				GUI.skin.button.active.background = buttonImage;
		
				buttonStyle = new GUIStyle (GUI.skin.button);
				buttonStyle.font = guiFont;
				buttonStyle.fontSize = 85;
				buttonStyle.alignment = TextAnchor.MiddleCenter;
		
				// display the display boxes if neccessary
				drawIntroBox ();
				drawOutroBox ();
				drawPauseBox ();

				// reset the resolution
				GUI.matrix = Matrix4x4.identity;
		}
	
		private void drawIntroBox ()
		{
				if (displayIntroBox) {
						if (currentIntro < Globals.WashingtonFightIntros [(int)levelCode - 10].Length - 1) {
								drawBox (Globals.WashingtonFightIntros [(int)levelCode - 10] [currentIntro], "Continue", 85);
								
								// create the start button
								buttonStyle.fontSize = 85;
								if (GUI.Button (buttonRect, buttonContent, buttonStyle)) {
										advanceIntro ();
								}
						} else {
								drawBox (Globals.WashingtonFightIntros [(int)levelCode - 10] [currentIntro], "Begin!", 85);
		
								// create the start button
								buttonStyle.fontSize = 85;
								if (GUI.Button (buttonRect, buttonContent, buttonStyle)) {
										startBattle ();
								}
						}
				}
		}

		private void drawPauseBox ()
		{
				if (Globals.paused && !displayIntroBox && !displayOutroBox) {
						opaqueBox ("Paused\n\n\nPress Start to resume", boxContent.text, 85);								
				}
		}

		private void advanceIntro ()
		{  
				if (currentIntro < Globals.WashingtonFightIntros [(int)levelCode - 10].Length - 1)
						currentIntro++;
				else
						startBattle ();
		}
		
		private void startBattle ()
		{
				displayIntroBox = false;
				Globals.paused = false;
				GameTimer.StartTimer ();
				//TODO play button noise
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.Joystick1Button7) || Input.GetKeyDown (KeyCode.Joystick1Button9)) {
						if (displayIntroBox)
								advanceIntro ();
						else if (displayOutroBox)
								endBattle ();
						else {
								Globals.paused = !Globals.paused;
						}
				}
		}
	
		private void drawOutroBox ()
		{
				if (Globals.GameState == BattleStateEnum.LOSE) {
						boxContent.text = "You Lost!";
						drawBox ("", boxContent.text, 60);
						displayOutroBox = true;
				} else if (Globals.GameState == BattleStateEnum.WIN) {
						boxContent.text = "You Won!";
						drawBox (Globals.WashingtonFightOutros [(int)levelCode - 10], boxContent.text, 60);
						displayOutroBox = true;
				} else {
						return;
				}

				Globals.paused = true;
				Globals.CurrentScene = levelCode;

				// create end battle button
				buttonStyle.fontSize = 85;
				if (GUI.Button (buttonRect, buttonContent, buttonStyle)) {
						endBattle ();
				}	
		}

		private void endBattle ()
		{
				Application.LoadLevel ("WashingtonMap");
				Globals.paused = true;
				//TODO play button sound
				//AudioSource audio = gameObject.AddComponent<AudioSource> ();
				//audio.clip = Resources.Load ("Sounds/Menu Select Sound") as AudioClip;
		}

		private void opaqueBox (string boxText, string buttonText, int fontSize)
		{
				GUI.color = new Color (1.0f, 1.0f, 1.0f, 0.75f);
				drawBox (boxText, buttonText, fontSize, new Rect (610, 300, 700, 600));
				GUI.color = new Color (1f, 1f, 1f, 1f);

		}

		private void drawBox (string boxText, string buttonText, int fontSize, Rect boxRect)
		{	
				// draw the box and text
				boxContent.text = boxText;
				boxStyle.fontSize = fontSize;
			
				GUI.DrawTexture (boxRect, boxImage, ScaleMode.StretchToFill);
				GUI.Label (new Rect (boxRect.x + 100, boxRect.y + 100, boxRect.width - 200, boxRect.height - 200), boxContent, boxStyle);
		
				// change the button text
				buttonContent.text = buttonText;
		}

		private void drawBox (string boxText, string buttonText, int fontSize)
		{	
				// draw the box and text
				boxContent.text = boxText;
				boxStyle.fontSize = fontSize;
			
				GUI.DrawTexture (boxRect, boxImage, ScaleMode.StretchToFill);
				GUI.Label (new Rect (boxRect.x + 100, boxRect.y + 100, boxRect.width - 200, boxRect.height - 200), boxContent, boxStyle);
		
				// change the button text
				buttonContent.text = buttonText;
		}
}
