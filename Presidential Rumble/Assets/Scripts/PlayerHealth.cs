using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

		public static int MaxHealth = 100;
		public static int CurrentHealth = 100;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{
				GUI.Box (new Rect (70, 30, Screen.width / 3 / (MaxHealth / CurrentHealth), 20), "George Washington");
				
		}

		public static void DecrementHealth ()
		{
				CurrentHealth--;
		}
}
