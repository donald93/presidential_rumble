using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum SceneEnum : int
{
	MAIN_MENU = 0,
	CHARACTER_SELECT = 1,
	//...
	TUTORIAL = 9,
	WASHINGTON_MAP = 10,
	WASHINGTON_LEVEL_1 = 11,
	WASHINGTON_LEVEL_2 = 12

	// pres 2 map = 20
};

public class Map : MonoBehaviour
{
	private GameObject[] buttons;
	private GameObject startButton;
	private int selected;
	private bool axisBusy;
	private TextMesh levelName;

	void Awake()
	{
		levelName = new TextMesh ();
		axisBusy = false;
		selected = 0;
		buttons = GameObject.FindGameObjectsWithTag ("MapButtonTag").OrderBy( button => button.transform.gameObject.GetComponent<MapButton>().Scene() ).ToArray();
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

		selected = selectedButton.GetComponent<MapButton>().Scene();

		startButton = GameObject.FindWithTag ("StartButtonTag");
		startButton.GetComponent<GUIButton> ().scene = (SceneEnum)selected;

		//Vector3 screenPos = camera.WorldToScreenPoint (selectedButton.transform.position);
		//levelName.text = selectedButton.GetComponent<MapButton> ().levelNameDisplay;
		//levelName.font =
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
