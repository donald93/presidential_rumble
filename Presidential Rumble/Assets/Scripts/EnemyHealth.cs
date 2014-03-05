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
			
				GUI.Box (new Rect (500, 30, 125 - (MaxHealth - CurrentHealth), 20), "French Soldier");
		
		}
	
		public static void DecrementHealth ()
		{
				CurrentHealth -= 1;
				if (CurrentHealth == 0)
						GameObject.Find ("EventListener").SendMessage ("NextScene", 3);
		}
}
