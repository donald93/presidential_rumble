using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;

/// <summary>
/// MapButton contains all of the information needed 
/// by the <see cref="Map"/> class to display level names and 
/// change levels.
/// </summary>
public class MapButton : MonoBehaviour
{
	[SerializeField]
	private SceneEnum m_scene;
	/// <summary>
	/// Gets or sets the scene. This is the level that 
	/// this MapButton will take you to. See <see cref="SceneEnum"/>.
	/// </summary>
	/// <value>
	/// The scene is a SceneEnum.
	/// </value>
	public SceneEnum Scene
	{
		get { return m_scene; }
		set { m_scene = value; }
	}

	[SerializeField]
	private string m_levelNameDisplay;
	/// <summary>
	/// Gets or sets the level name display. This will be used 
	/// to show the name of the level 
	/// (i.e. "Washington's Cherry Tree").
	/// </summary>
	/// <value>
	/// The string that will be displayed on screen.
	/// </value>
	public string levelNameDisplay
	{
		get { return m_levelNameDisplay; }
		set { m_levelNameDisplay = value; }
	}

	[SerializeField]
	private bool m_unlocked;
	/// <summary>
	/// Gets or sets a value indicating whether this 
	/// <see cref="MapButton"/> is unlocked. Only unlocked levels
	/// should be available to select.
	/// </summary>
	/// <value>
	/// <c>true</c> if unlocked; otherwise, <c>false</c>.
	/// </value>
	public bool unlocked
	{
		get { return m_unlocked; }
		set { m_unlocked = value; }
	}
}
