using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {
	private float prev_time;
	public float time;
	public Font TimerFont;

	// Use this for initialization
	void Start () {
		prev_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
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
	{	Vector2 ratio = new Vector2(Screen.width/Globals.originalWidth , Screen.height/Globals.originalHeight );
		Matrix4x4 guiMatrix = Matrix4x4.identity;
		guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
		GUI.matrix = guiMatrix;

		GUI.skin.GetStyle ("Label").fontSize = 85;
		GUI.skin.GetStyle ("Label").alignment = TextAnchor.LowerCenter;
		GUI.skin.GetStyle ("Label").font = TimerFont;

		GUI.contentColor = Color.black;
		int width = 100;
		int height;

		GUI.Label (new Rect (Screen.width/2 - width/2, 20, width, 100), Mathf.FloorToInt(time).ToString ());

		width = Screen.width - 100;
		height = Screen.height - 100;
		GUI.skin.GetStyle ("Box").fontSize = 85;
		GUI.skin.GetStyle ("Box").alignment = TextAnchor.MiddleCenter;
		GUI.skin.GetStyle ("Box").font = TimerFont;
		if (time == 0)
		{
			GUI.Box(new Rect (Screen.width/2 -width/2,Screen.height/2 - height/2,width, height), "Time Over");
			GUIButton guiButton = gameObject.AddComponent<GUIButton>();
			guiButton.x = 810;
			guiButton.y = 540;
			guiButton.width = 1000;
			guiButton.height = 100;
			guiButton.text = "Main Menu";
			guiButton.scene = SceneEnum.MainMenu;
			//GUIButton retry = new GUIButton(0,0,320,120,null,null,null,"Main Menu",null,SceneEnum.MainMenu);
			//stop controls
		}	
		GUI.matrix = Matrix4x4.identity;
	}
}
