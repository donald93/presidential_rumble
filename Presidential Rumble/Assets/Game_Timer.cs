using UnityEngine;
using System.Collections;

public class Game_Timer : MonoBehaviour {
	private float prev_time;
	public float time;

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
	{
		GUI.skin.GetStyle ("Label").fontSize = 60;
		GUI.contentColor = Color.black;
		int width = 100;
		GUI.Label (new Rect (Screen.width/2 - width/2, 0, width, 100), Mathf.FloorToInt(time).ToString ());

		if (time == 0)
		{
			//GUI.Box(
			//GUIButton retry = new GUIButton
			//stop controls
		}
	}
}
