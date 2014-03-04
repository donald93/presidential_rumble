using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
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
				GUI.Box (new Rect (10, 10, Screen.width / 2 / (MaxHealth / CurrentHealth), 20), "" + CurrentHealth);
		
		}
	
		public static void DecrementHealth ()
		{
				CurrentHealth--;
		}
}
