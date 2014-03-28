using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour
{

		public Font timerFont;
		public GUIButton startButton;
		public Texture2D boxImage;

		private bool displayIntroBox;
		private GUIStyle boxStyle;
		private GUIContent boxContent;
		private Rect boxRect;
		// Use this for initialization
		void Start ()
		{
	

				boxStyle = new GUIStyle ();
				boxStyle.fontSize = 85;
				boxStyle.alignment = TextAnchor.MiddleCenter;
				boxStyle.font = timerFont;
				boxStyle.wordWrap = true;

				boxContent = new GUIContent ();

				boxRect = new Rect (100, 100, Globals.originalWidth - 200, Globals.originalHeight - 200);

				displayIntroBox = true;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}


		void OnGUI ()
		{
				GUI.matrix = Globals.PrepareMatrix ();

				drawIntroBox ();
				drawOutroBox ();
					
				// reset the resolution
				GUI.matrix = Matrix4x4.identity;
		}

		public void drawOutroBox ()
		{
				if (Globals.GameState == BattleStateEnum.ONGOING)
						return;
				if (Globals.GameState == BattleStateEnum.LOSE) {
						boxContent.text = "You Lost!";
				} else if (Globals.GameState == BattleStateEnum.TIE) {
						boxContent.text = "You Tied!";
				} else { // win
						boxContent.text = "You Won!";
				}
				// prepare to draw the time's up box
		
				GUI.Box (boxRect, boxContent, boxStyle);
		
				//create the retry button

				GUIButton guiButton = gameObject.AddComponent<GUIButton> ();
				guiButton.width = 400;
				guiButton.height = 120;
				guiButton.x = Globals.originalWidth / 2;
				guiButton.y = Globals.originalHeight / 2;
				guiButton.text = "Main Menu";
				guiButton.scene = SceneEnum.MainMenu;
		
				GUI.DrawTexture (boxRect, boxImage, ScaleMode.StretchToFill);

				GUI.Label (boxRect, boxContent, boxStyle);
				if (GUI.Button (new Rect (200, 400, 800, 200), "Main Menu")) {
						Application.LoadLevel ("WashingtonMap");
				}	
				AudioSource audio = gameObject.AddComponent<AudioSource> ();
				audio.clip = Resources.Load ("Sounds/Menu Select Sound") as AudioClip;
		}

		void drawIntroBox ()
		{
				if (displayIntroBox) {
						boxContent.text = "This is the intro stuff";

						GUI.DrawTexture (boxRect, boxImage, ScaleMode.StretchToFill);

						GUI.Label (boxRect, boxContent, boxStyle);
						if (GUI.Button (new Rect (200, 400, 800, 200), "Start")) {
								displayIntroBox = false;
								Globals.paused = false;
								GameTimer.StartTimer ();
						}
				}
		}
}
