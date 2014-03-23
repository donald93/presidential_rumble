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

	private bool displayIntroBox;

	// Use this for initialization
	void Start ()
	{
		prev_time = Time.time;

		displayIntroBox = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
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

		GUI.contentColor = Color.black;
		int width = 100;
		int height = 20;

		GUI.Label (new Rect (Globals.originalWidth/2 - width/2, height, width, 100), Mathf.FloorToInt(time).ToString ());

		if (time == 0)
		{
			// prepare to draw the time's up box
			width = Screen.width - 100;
			height = Screen.height - 100;
			GUI.skin.GetStyle ("Box").fontSize = 85;
			GUI.skin.GetStyle ("Box").alignment = TextAnchor.MiddleCenter;
			GUI.skin.GetStyle ("Box").font = timerFont;

			GUI.Box (new Rect (Globals.originalWidth/2 - width/2, Globals.originalHeight/2 - height/2, width, height), "Time Over");

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

		if (displayIntroBox)
		{
			GUI.Box(new Rect(100, 100, Globals.originalWidth - 200, Globals.originalHeight - 200), "here goes some text");
			if (GUI.Button (new Rect (200,400,800,200), "Start"))
			{
				displayIntroBox = false;
			}
		}

		// reset the resolution
		GUI.matrix = Matrix4x4.identity;
	}
}
