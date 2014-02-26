using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour
{

	GUIContent content = new GUIContent();
	public Texture2D defaultImage, hoverImage, downClickImage;
	public string text;
	public SceneEnum scene;
	public float x, y, width, height;

	void Awake()
	{
		content.text = text;
	}

	void OnGUI()
	{
		GUI.skin.button.normal.background = (Texture2D)defaultImage;
		GUI.skin.button.hover.background = (Texture2D)hoverImage;
		GUI.skin.button.active.background = (Texture2D)defaultImage;

		if (GUI.Button(new Rect((Screen.width * x) - (Screen.width * width)/2, (Screen.height * y) - (Screen.height * height)/2, Screen.width * width, Screen.height * height), content))
	    {
			Application.LoadLevel ((int)scene);
		}
	}
}
