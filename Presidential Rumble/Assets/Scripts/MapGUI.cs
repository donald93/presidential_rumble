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
public class MapGUI : MonoBehaviour
{
		private int selected;
		private bool axisBusy;
		private string levelName;
		private GUIStyle mapHudStyle;
		private GUIStyle vsStyle;
		private AudioSource mapAudio;

		public GameObject[] buttons;
		public Font font;
		public Texture2D guiImage;
		public GUIButton startButton;
		public GUIButton backButton;
		public AudioClip sound;

		void Awake ()
		{
				axisBusy = false;
				selected = 0;

				UnselectAll ();

				int i = 0;
				foreach (GameObject b in buttons) {
						// set up the font for each button
						b.GetComponent<MapButton> ().style.font = font;
						b.GetComponent<MapButton> ().style.alignment = TextAnchor.MiddleLeft;
						b.GetComponent<MapButton> ().style.wordWrap = true;

						// select the highest unlocked level
						if (!b.GetComponent<MapButton> ().levelLocked) {
								Select (b);
								selected = i;
						}

						i++;
				}

				mapHudStyle = new GUIStyle ();
				mapHudStyle.font = font;
				mapHudStyle.fontSize = 72;
				mapHudStyle.alignment = TextAnchor.MiddleCenter;
				mapHudStyle.wordWrap = true;

				mapAudio = gameObject.AddComponent<AudioSource> ();
				mapAudio.clip = sound;

				int level = (int)Globals.CurrentScene;
				if (level == 9)
						level = 0;
				else if (Globals.GameState == BattleStateEnum.WIN && (int)Globals.CurrentScene != 14)
						level = (int)Globals.CurrentScene - 10;
				else
						level = (int)Globals.CurrentScene - 11;

				Select (buttons [level]);
				while (level >= 0) {
						buttons [level--].GetComponent<MapButton> ().levelLocked = false;
				}

				Globals.CurrentScene = SceneEnum.WashingtonMap;
		}

		void OnGUI ()
		{
				// scale the GUI to the current screen size
				GUI.matrix = Globals.PrepareMatrix ();

				drawMapHud ();
				drawButtonLabels ();

				// reset the resolution
				GUI.matrix = Matrix4x4.identity;
		}

		void drawMapHud ()
		{
				int groupWidth = 600;
				GUI.BeginGroup (new Rect (0, 0, groupWidth, Globals.originalHeight));

				GUI.DrawTexture (new Rect (0, 0, groupWidth, Globals.originalHeight), guiImage, ScaleMode.StretchToFill);

				// make a smaller font for the vs label
				vsStyle = new GUIStyle (mapHudStyle);
				vsStyle.fontSize = 45;

				// draw the labels
				GUI.Label (new Rect (0, 0, groupWidth, 250), levelName, mapHudStyle);
				GUI.Label (new Rect (0, 300, groupWidth, 100), "VS", vsStyle);

				// draw the buttons
				Texture2D player1Image = buttons [selected].GetComponent<MapButton> ().player1Image;
				Texture2D player2Image = buttons [selected].GetComponent<MapButton> ().player2Image;

				// draw the player portraits
				GUI.DrawTexture (new Rect (200 - player1Image.width / 2, 330 - player1Image.height / 2, player1Image.width, player1Image.height), player1Image);
				GUI.DrawTexture (new Rect (400 - player2Image.width / 2, 330 - player2Image.height / 2, player2Image.width, player2Image.height), player2Image);

				GUI.EndGroup ();
		}

		void drawButtonLabels ()
		{
				// set the GUI images and font
				GUI.contentColor = Color.black;
				foreach (GameObject b in buttons) {
						if (!b.GetComponent<MapButton> ().levelLocked) {
								GUI.Label (new Rect (Globals.originalWidth / 2 + b.transform.position.x, -b.transform.position.y + Globals.originalHeight / 2, 0, 0), b.GetComponent<MapButton> ().levelNameDisplay, b.GetComponent<MapButton> ().style);
						}
				}
		}

		void Update ()
		{
				// cheat unlock all
				if (Input.GetKeyDown ("0")) {
						foreach (GameObject b in buttons) {
								b.GetComponent<MapButton> ().levelLocked = false;
						}
				}
				
				// start selected level
				else if (Input.GetAxisRaw("Enter") != 0) {
						startButton.changeScenes ();
				}

				// go back
				else if (Input.GetAxisRaw("Back") != 0) {
						backButton.changeScenes ();
				}
		         
				// check for input
				if (Input.GetMouseButtonDown (0)) {
						axisBusy = false;
						RaycastHit hit;
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

						if (Physics.Raycast (ray, out hit, 100.0f)) {
								GameObject hitButton = hit.transform.gameObject;
								if (null != hitButton.GetComponent<MapButton> () && !hitButton.GetComponent<MapButton> ().levelLocked) {
										int i = 0;
										while (i < buttons.Length) {
												if (buttons [i] == hitButton) {
														selected = i;
														break;
												}
												i++;
										}

										Select (hit.transform.gameObject);
										mapAudio.Play ();
								}
						}
				} else {  
						if (Input.GetAxisRaw ("Horizontal") != 0) {
								if (!axisBusy) {
										axisBusy = true;
										if (Input.GetAxisRaw ("Horizontal") < 0) {
												if (--selected < 0 || buttons [selected].GetComponent<MapButton> ().levelLocked) {
														selected = 0;
												}
										} else {
												if (++selected > buttons.Length - 1 || buttons [selected].GetComponent<MapButton> ().levelLocked) {
														selected--;
												}
										}

										Select (buttons [selected]);
										mapAudio.PlayOneShot (sound);
								}
						}
						if (Input.GetAxisRaw ("Horizontal") == 0) {
								axisBusy = false;
						}
				}
		}

		void Select (GameObject selectedButton)
		{
				UnselectAll ();

				// make the selected button stand out
				selectedButton.transform.position = new Vector3 (selectedButton.transform.position.x, selectedButton.transform.position.y, 0);
				selectedButton.GetComponent<MapButton> ().style.fontSize = 72;

				// set the gui stuff for beginning the level
				startButton.GetComponent<GUIButton> ().scene = selectedButton.GetComponent<MapButton> ().scene;
				levelName = selectedButton.GetComponent<MapButton> ().levelNameDisplay;
		}

		void UnselectAll ()
		{
				foreach (GameObject b in buttons) {
						b.transform.position = new Vector3 (b.transform.position.x, b.transform.position.y, 80);
						b.GetComponent<MapButton> ().style.fontSize = 40;
				}
		}
}
