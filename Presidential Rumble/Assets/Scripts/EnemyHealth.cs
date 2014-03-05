using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	
		public static int MaxHealth = 125;
		public static int CurrentHealth = 125;
	
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
				GUI.Box (new Rect (320, 20, 125 - (MaxHealth - CurrentHealth), 12), "");
		
		}
	
		public static void DecrementHealth ()
		{
				CurrentHealth--;
				
		}
}
