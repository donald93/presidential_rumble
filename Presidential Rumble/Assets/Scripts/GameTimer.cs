﻿using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
		private static float prev_time;
		public float time;
		public Font timerFont;
		public GUIButton startButton;
		public Texture2D boxImage;

		private GUIStyle boxStyle;

		// Use this for initialization
		void Start ()
		{
				//prev_time = Time.time;

				boxStyle = new GUIStyle ();
				boxStyle.fontSize = 85;
				boxStyle.alignment = TextAnchor.MiddleCenter;
				boxStyle.font = timerFont;
				boxStyle.wordWrap = true;
		}

		public static void StartTimer ()
		{
				GameTimer.prev_time = Time.time;
		}

		// Update is called once per frame
		void Update ()
		{
				if (!Globals.paused) {
						//TODO this does not belong in the view
						if (time > 0) {
								time -= Time.time - prev_time;
								GameTimer.prev_time = Time.time;
						}

						if (time <= 0) {
								time = 0;
								Globals.GameState = BattleStateEnum.LOSE;
						}
				}
				GameTimer.prev_time = Time.time;
		}
		
		void OnGUI ()
		{
				GUI.matrix = Globals.PrepareMatrix ();

				// prepare the label tag for the timer
				GUI.skin.GetStyle ("Label").fontSize = 85;
				GUI.skin.GetStyle ("Label").alignment = TextAnchor.LowerCenter;
				GUI.skin.GetStyle ("Label").font = timerFont; 

				//GUI.contentColor = Color.black;
				int width = 100;
				int height = 20;
				GUI.Box (new Rect ((Globals.originalWidth / 2 - width / 2), height, width, 90), "");
				GUI.Label (new Rect (Globals.originalWidth / 2 - width / 2, height, width, 100), Mathf.FloorToInt (time).ToString ());
		}
}
