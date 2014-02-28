using UnityEngine;
using System.Collections;

/// <summary>
/// GUIButton should be attached to an empty object.
/// All location and scale variables are based on 
/// the original 1920x1080 sceme. The GUI is then 
/// scaled to match the current screen resolution.
/// </summary>
public class GUIButton : MonoBehaviour
{
	public float x, y, width, height;
	public Texture2D defaultImage, hoverImage, downClickImage;
	public string text;
	public Font font;
	public SceneEnum scene;

	public static readonly float originalWidth = 1920;
	public static readonly float originalHeight = 1080;

	public GUIButton(float x, float y, float width, float height, Texture2D defaultImage, Texture2D hoverImage, Texture2D downClickImage, string text, Font font, SceneEnum scene)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.defaultImage = defaultImage;
		this.hoverImage = hoverImage;
		this.downClickImage = downClickImage;
		this.text = text;
		this.font = font;
		this.scene = scene;
	}

	void OnGUI()
	{
		// scale the GUI to the current screen size
		Vector2 ratio = new Vector2(Screen.width/originalWidth , Screen.height/originalHeight );
		Matrix4x4 guiMatrix = Matrix4x4.identity;
		guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
		GUI.matrix = guiMatrix;

		// set the GUI images and font
		GUI.skin.button.normal.background = (Texture2D)defaultImage;
		GUI.skin.button.hover.background = (Texture2D)hoverImage;
		GUI.skin.button.active.background = (Texture2D)defaultImage;
		GUI.skin.font = font;
		GUI.skin.GetStyle("Button").fontSize = Mathf.FloorToInt(0.6f * height);
		GUI.depth = 0;
		// draw the button
		if (GUI.Button(new Rect(x - width/2, y - height/2, width, height), text))
	    {
			Application.LoadLevel (scene.ToString());
		}

		// reset the resolution
		GUI.matrix = Matrix4x4.identity;
	}
}
