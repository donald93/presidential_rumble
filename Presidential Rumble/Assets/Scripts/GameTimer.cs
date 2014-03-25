using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
	private float prev_time;
	public float time;
	public Font timerFont;
	public Texture2D player1Portrait;
	public Texture2D player2Portrait;
	public GUIButton startButton;
	public Texture2D boxImage;

	private bool displayIntroBox;
	private GUIStyle boxStyle;
	private GUIContent boxContent;
	private Rect boxRect;

	// Use this for initialization
	void Start ()
	{
		prev_time = Time.time;

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
		//TODO this does not belong in the view
		if ( time > 0)
		{
			time -= Time.time - prev_time;
			prev_time = Time.time;
		}

		if (time <= 0)
		{
			time = 0;
		}
	}

	void OnGUI()
	{
		// scale the GUI to the current screen size
		GUI.matrix = Globals.PrepareMatrix();

		// draw the player portraits
		GUI.DrawTexture (new Rect (70, 85, 120, 120), player1Portrait);
		GUI.DrawTexture (new Rect (1720, 85, 120, 120), player2Portrait);

		// prepare the label tag for the timer
		GUI.skin.GetStyle ("Label").fontSize = 85;
		GUI.skin.GetStyle ("Label").alignment = TextAnchor.LowerCenter;
		GUI.skin.GetStyle ("Label").font = timerFont; 

		//GUI.contentColor = Color.black;
		int width = 100;
		int height = 20;

		GUI.Label (new Rect (Globals.originalWidth/2 - width/2, height, width, 100), Mathf.FloorToInt(time).ToString ());

		if (time == 0)
		{
			drawOutroBox (BattleStateEnum.TIE);
		}

		drawIntroBox ();

		// reset the resolution
		GUI.matrix = Matrix4x4.identity;
	}

	void drawOutroBox (BattleStateEnum endState)
	{
		if (endState == BattleStateEnum.LOSE)
		{
			boxContent.text = "You Lost!";
		}
		else if (endState == BattleStateEnum.TIE)
		{
			boxContent.text = "You Tied!";
		}
		else // win
		{
			boxContent.text = "You Won!";
		}

		// prepare to draw the time's up box
		
		GUI.Box (boxRect, boxContent, boxStyle);
		
		// create the retry button
		GUIButton guiButton = gameObject.AddComponent<GUIButton>();
		guiButton.width = 400;
		guiButton.height = 120;
		guiButton.x = Globals.originalWidth/2;
		guiButton.y = Globals.originalHeight/2;
		guiButton.text = "Main Menu";
		guiButton.scene = SceneEnum.MainMenu;
		
		
		AudioSource audio = gameObject.AddComponent<AudioSource>();
		audio.clip = Resources.Load("Sounds/Menu Select Sound") as AudioClip;
		//TODO stop controls
	}

	void drawIntroBox ()
	{
		if (displayIntroBox)
		{
			boxContent.text = "This is the intro stuff";

			GUI.DrawTexture (boxRect, boxImage, ScaleMode.StretchToFill);

			GUI.Label (boxRect, boxContent, boxStyle);
			if (GUI.Button (new Rect (200,400,800,200), "Start"))
			{
				displayIntroBox = false;
				//TODO start the battle
			}
		}
	}
}
