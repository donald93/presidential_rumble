using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;

/// <summary>
/// MapButton contains all of the information needed 
/// by the <see cref="Map"/> class to display level names and 
/// change levels.
/// </summary>
/// <remarks>
/// This is meant to be attached to a <see cref="GameObject"/> 
/// that contains information to switch levels.
/// </remarks>
public class MapButton : MonoBehaviour
{
	/// <summary>
	/// The scene is the level that this MapButton
	/// will take you to. See <see cref="SceneEnum"/>.
	/// </summary>
	public SceneEnum scene;

	/// <summary>
	/// The level name display will be used 
	/// to show the name of the level 
	/// (i.e. "Washington's Cherry Tree").
	/// </summary>
	public string levelNameDisplay;

	/// <summary>
	/// Whether or not this <see cref="MapButton"/>
	/// is unlocked. Only unlocked levels should
	/// be available to select.
	/// </summary>
	public bool levelLocked;

	/// <summary>
	/// The style is used by the <see cref="levelNameDisplay"/>
	/// to format the text that will be displayed.
	/// </summary>
	public GUIStyle style;

	public Texture2D player1Image;

	public Texture2D player2Image;
}
