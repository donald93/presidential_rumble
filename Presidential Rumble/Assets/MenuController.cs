using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	public GameObject[] buttons;

	private bool axisBusy;
	private int selected;

	void Awake() {
		selected = 0;
		axisBusy = false;

		Select (buttons [selected]);
	}

	// Update is called once per frame
	void Update () {
		// get controller movement
		if (Input.GetAxisRaw ("Horizontal") != 0) {
			if (!axisBusy) {
				if (selected == -1)
					selected = 0;
				else {
					if (selected % 2 == 0)
						selected++;
					else
						selected--;
				}

				Screen.showCursor = false;
				Screen.lockCursor = true;
				axisBusy = true;
				Select (buttons [selected]);
			}
		}
		else if (Input.GetAxisRaw ("Vertical") != 0) {
			if (!axisBusy) {
				if (selected == -1)
					selected = 0;
				else {
					if (selected < 2)
						selected += 2;
					else
						selected -= 2;
				}

				Screen.showCursor = false;
				Screen.lockCursor = true;
				axisBusy = true;
				Select (buttons [selected]);
			}
		}
		else if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0) {
			axisBusy = true;
			Screen.showCursor = true;
			Screen.lockCursor = false;
			selected = -1;
			UnselectAll ();
		}
		else {
			axisBusy = false;
		}

		//TODO get controller buttons
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
