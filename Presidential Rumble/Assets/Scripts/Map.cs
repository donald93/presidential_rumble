using UnityEngine;
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

	void Awake ()
	{
		axisBusy = false;
		selected = 0;

		foreach (GameObject b in buttons)
		{
			b.AddComponent<GUIText>();
			b.guiText.text = b.GetComponent<MapButton>().levelNameDisplay;
			b.guiText.font = font;
			b.guiText.fontSize = 55;
			b.guiText.pixelOffset = new Vector2 (0, 0);
		}

		//TODO what do i do with this?
		mapAudio = gameObject.AddComponent<AudioSource> ();
		mapAudio.clip = sound;

		Select (buttons[0]);
	}

	void OnGUI ()
	{
		// scale the GUI to the current screen size
		GUI.matrix = Globals.PrepareMatrix ();

		// set the GUI images and font
		GUI.skin.font = font;
		GUI.skin.GetStyle ("Label").fontSize = 72;
		GUI.skin.GetStyle ("Label").alignment = TextAnchor.MiddleLeft;
		foreach (GameObject b in buttons)
		{
			GUI.Label(new Rect (Globals.originalWidth/2 + b.transform.position.x, b.transform.position.y, 500, 500), b.GetComponent<MapButton>().levelNameDisplay);
		}

		// set the GUI images and font
		GUI.skin.font = font;
		GUI.skin.GetStyle ("Label").fontSize = 72;
		GUI.skin.GetStyle ("Label").alignment = TextAnchor.LowerCenter;

		int groupWidth = 600;
		int leftX = 0;
		GUI.BeginGroup (new Rect (leftX, 0, groupWidth, Globals.originalHeight));
		GUI.Box (new Rect (0, 0, groupWidth, Globals.originalHeight), guiImage);
		GUI.Label (new Rect (0, 0, groupWidth, 100), levelName);

		startButton.x = leftX + 300;
		startButton.y = 300;
		backButton.x = leftX + 300;
		backButton.y = 800;

		GUI.EndGroup ();

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
				if (hitButton.CompareTag ("MapButtonTag") && !hitButton.GetComponent<MapButton> ().locked)
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

		// do the special stuff for the selected button
		if (!selectedButton.GetComponent ("Halo"))
		{
			selectedButton.transform.gameObject.AddComponent ("Halo");
		}

		selected = (int)selectedButton.GetComponent<MapButton> ().Scene;

		startButton.GetComponent<GUIButton> ().scene = (SceneEnum)selected;

		levelName = selectedButton.GetComponent<MapButton> ().levelNameDisplay;
	}

	void UnselectAll ()
	{
		foreach (GameObject button in buttons)
		{
			if (button.GetComponent ("Halo"))
			{
				Destroy (button.GetComponent ("Halo"));
			}
		}
	}
}
