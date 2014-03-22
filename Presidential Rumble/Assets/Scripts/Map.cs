﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Map contains all of the logic of the level selection screen. 
/// It will bring to attention all <see cref="MapButton"/> that 
/// have been unlocked with special focus on the currently selected 
/// map button.  It also sets up the GUI that you can use to change 
/// scenes.
/// </summary>
public class Map : MonoBehaviour
{
	public GameObject[] buttons;
	private int selected;
	private bool axisBusy;
	private string levelName;

	private AudioSource mapAudio;

	public Font font;
	public Texture2D guiImage;
	public GUIButton startButton;
	public GUIButton backButton;
	public AudioClip sound;

	private GUIStyle myStyle;

	void Awake ()
	{
		axisBusy = false;
		selected = 0;

		foreach (GameObject b in buttons)
		{
			b.GetComponent<MapButton>().style.font = font;
			b.GetComponent<MapButton>().style.alignment = TextAnchor.MiddleLeft;
		}

		//TODO what do i do with this?
		mapAudio = gameObject.AddComponent<AudioSource> ();
		mapAudio.clip = sound;

		myStyle = new GUIStyle ();
		myStyle.normal.textColor = Color.black;

		UnselectAll ();
		Select (buttons[0]);
	}

	void OnGUI ()
	{
		// scale the GUI to the current screen size
		GUI.matrix = Globals.PrepareMatrix ();

		// set the GUI images and font
		//GUI.skin.font = font;
		//GUI.skin.GetStyle ("Label").fontSize = 72;
		//GUI.skin.GetStyle ("Label").alignment = TextAnchor.MiddleCenter;
		GUIStyle titleStyle = new GUIStyle ();
		titleStyle.font = font;
		titleStyle.fontSize = 72;
		titleStyle.alignment = TextAnchor.MiddleCenter;
		titleStyle.wordWrap = true;
		
		int groupWidth = 600;
		GUI.BeginGroup (new Rect (0, 0, groupWidth, Globals.originalHeight));
		//GUI.Box (new Rect (0, 0, groupWidth, Globals.originalHeight), guiImage);
		GUI.DrawTexture (new Rect (0, 0, groupWidth, Globals.originalHeight), guiImage, ScaleMode.StretchToFill);
		GUI.Label (new Rect (0, 0, groupWidth, 250), levelName, titleStyle);
		
		startButton.x = 300;
		startButton.y = 300;
		backButton.x = 300;
		backButton.y = 800;
		
		GUI.EndGroup ();

		// set the GUI images and font
		GUI.contentColor = Color.black;
		foreach (GameObject b in buttons)
		{
			GUI.Label(new Rect (Globals.originalWidth/2 + b.transform.position.x, -b.transform.position.y + Globals.originalHeight/4, 500, 500), b.GetComponent<MapButton>().levelNameDisplay, b.GetComponent<MapButton>().style);
		}

		// reset the resolution
		GUI.matrix = Matrix4x4.identity;
	}

	void Update ()
	{
		// check for input
		if (Input.GetMouseButtonDown (0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, 100.0f))
			{
				GameObject hitButton = hit.transform.gameObject;
				if (null != hitButton.GetComponent<MapButton>() && !hitButton.GetComponent<MapButton> ().locked)
				{
					Select (hit.transform.gameObject);
					mapAudio.Play ();
				}
			}
		}
		else
		{
			if (Input.GetAxisRaw ("Horizontal") != 0)
			{
				if (!axisBusy)
				{
					axisBusy = true;
					if (++selected > buttons.Length - 1 || buttons [selected].GetComponent<MapButton> ().locked)
					{
						selected = 0;
					}

					Select (buttons [selected]);
					mapAudio.PlayOneShot (sound);
				}
			}
			if (Input.GetAxisRaw ("Horizontal") == 0)
			{
					axisBusy = false;
			}
		}
	}

	void Select (GameObject selectedButton)
	{
		UnselectAll ();
		
		// make the selected button stand out
		selectedButton.transform.position = new Vector3(selectedButton.transform.position.x, selectedButton.transform.position.y, 0);
		selectedButton.GetComponent<MapButton>().style.fontSize = 72;

		// set the gui stuff for beginning the level
		startButton.GetComponent<GUIButton> ().scene = selectedButton.GetComponent<MapButton> ().Scene;
		levelName = selectedButton.GetComponent<MapButton> ().levelNameDisplay;
	}

	void UnselectAll ()
	{
		foreach (GameObject b in buttons)
		{
			b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, 75);
			b.GetComponent<MapButton>().style.fontSize = 40;
		}
	}
}
