using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;

public class MapButton : MonoBehaviour
{
	public SceneEnum scene;
	public string levelNameDisplay;
	private bool unlocked;

	public int Scene()
	{
		return (int)scene;
	}
}
