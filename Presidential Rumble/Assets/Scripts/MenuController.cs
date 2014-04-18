﻿using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	public GameObject[] buttons;

	private bool axisBusy;
	private int selected;
	private int fullColumns;

	void Awake() {
		selected = -1;
		axisBusy = false;

		fullColumns = Mathf.CeilToInt (Mathf.Sqrt (buttons.Length));
	}

	// Update is called once per frame
	void Update () {
		// get controller movement
		if (Input.GetAxisRaw ("Horizontal") != 0) {
			if (!axisBusy) {
				if (selected >= 0) {
					if (Input.GetAxisRaw ("Horizontal") < 0) {
						if (selected % fullColumns == 0) {
							selected += (fullColumns - 1);
						}
						else {
							selected--;
						}

						while (selected >= buttons.Length) {
							selected--;
						}
					}
					else {
						selected++;

						if (selected % fullColumns == 0) {
							selected -= fullColumns;
						}

						if (selected > buttons.Length - 1) {
							while (selected % fullColumns != 0) {
								selected++;
							}
							selected -= fullColumns;
						}
					}
				}
				else {
					selected = 0;
				}

				Screen.showCursor = false;
				Screen.lockCursor = true;
				axisBusy = true;
				Select (buttons [selected]);
			}
		}
		else if (Input.GetAxisRaw ("Vertical") != 0) {
			if (!axisBusy) {
				if (selected >= 0) {
					if (Input.GetAxisRaw ("Vertical") < 0) {
						selected -= fullColumns;

						if (selected < 0) {
							selected += fullColumns * fullColumns;
						}

						if (selected >= buttons.Length) {
							selected -= fullColumns;
						}
					}
					else {
						selected += fullColumns;

						if (selected >= buttons.Length) {
							selected = selected % fullColumns;
						}
					}
				}
				else {
					selected = 0;
				}

				Screen.showCursor = false;
				Screen.lockCursor = true;
				axisBusy = true;
				Select (buttons [selected]);
			}
		}
		else if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0) {
			//axisBusy = true;
			Screen.showCursor = true;
			Screen.lockCursor = false;
			selected = -1;
			UnselectAll ();
		}
		else {
			axisBusy = false;
		}

		if (Input.GetAxisRaw("Enter") != 0 && selected >= 0) {
			Screen.showCursor = true;
			Screen.lockCursor = false;
			buttons [selected].GetComponent<GUIButton>().changeScenes ();
		}
	}

	private void Select (GameObject button) {
		UnselectAll ();

		button.GetComponent<GUIButton>().defaultImage = button.GetComponent<GUIButton>().hoverImage;
		//mapAudio.PlayOneShot (sound);
	}

	private void UnselectAll () {
		foreach (GameObject button in buttons) {
			button.GetComponent<GUIButton>().defaultImage = button.GetComponent<GUIButton>().downClickImage;
		}
	}
}