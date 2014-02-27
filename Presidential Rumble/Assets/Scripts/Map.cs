using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// SceneEnum represents a level for each name.
/// This name must match the name of the scene 
/// in the Assets->Scenes folder.
/// 
/// For simplicity's sake, the int values are 
/// incremented by 10 for each important section.
/// </summary>
public enum SceneEnum
{
	MainMenu = 0,
	Main = 1, //TODO test scene only
	//CharacterSelect = 1,
	//AboutDevelopers = 2,
	//...
	//Tutorial = 9,
	WashingtonMap = 10,
	//WashingtonLevel1 = 11,
	//WashingtonLevel2 = 12,

	// pres 2 map = 20
};

/// <summary>
/// Map contains all of the logic of the level selection screen. 
/// It will bring to attention all <see cref="MapButton"/> that 
/// have been unlocked with special focus on the currently selected 
/// map button.  It also sets up the GUI that you can use to change 
/// scenes.
/// </summary>
public class Map : MonoBehaviour
{
	private GameObject[] buttons;
	private GameObject startButton;
	private int selected;
	private bool axisBusy;
	private string levelName;

	void Awake()
	{
		axisBusy = false;
		selected = 0;
		buttons = GameObject.FindGameObjectsWithTag ("MapButtonTag").OrderBy( 
			button => button.transform.gameObject.GetComponent<MapButton>().Scene ).ToArray();
		startButton = GameObject.FindWithTag ("StartButtonTag");

		//TODO select highest unlocked level
	}

	void OnGUI()
	{
		GUI.Label (new Rect (0, 0, 100, 100), levelName);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0))
		{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out hit, 100.0f))
				{
						if (hit.transform.gameObject.CompareTag ("MapButtonTag"))
						{
								Select (hit.transform.gameObject);
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
					if (++selected > buttons.Length - 1)
					{
						selected = 0;
					}

					Select (buttons [selected]);
				}
			}
			if (Input.GetAxisRaw ("Horizontal") == 0)
			{
				axisBusy = false;
			}
		}
	}

	void Select(GameObject selectedButton)
	{
		UnselectAll ();

		if (!selectedButton.GetComponent("Halo"))
			selectedButton.transform.gameObject.AddComponent("Halo");

		selected = (int)selectedButton.GetComponent<MapButton>().Scene;

		startButton.GetComponent<GUIButton> ().scene = (SceneEnum)selected;

		levelName = selectedButton.GetComponent<MapButton> ().levelNameDisplay;
	}

	void UnselectAll()
	{
		foreach (GameObject button in buttons)
		{
			if (button.GetComponent("Halo"))
				Destroy(button.GetComponent("Halo"));
		}
	}
}
