using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	
	public static int MaxHealth;
	public static int CurrentHealth;
	
		// Use this for initialization
		void Start ()
		{
		CurrentHealth = 125;
		MaxHealth= 125;

		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}
	
		void OnGUI ()
		{
<<<<<<< HEAD
				GUI.Box (new Rect (320, 20, 125 - (MaxHealth - CurrentHealth), 12), "");
=======
			
				GUI.Box (new Rect (500, 30, 125 - (MaxHealth - CurrentHealth), 20), "French Soldier");
>>>>>>> FETCH_HEAD
		
		}
	
		public static void DecrementHealth ()
		{
<<<<<<< HEAD
				CurrentHealth -= 4;
				if (CurrentHealth <= 0)
=======
<<<<<<< HEAD
				CurrentHealth--;
				
=======
				CurrentHealth -= 1;
				if (CurrentHealth == 0)
>>>>>>> ea5b796e085f232ec7c5bd257c58a7519ba66b62
						GameObject.Find ("EventListener").SendMessage ("NextScene", 3);
>>>>>>> FETCH_HEAD
		}
}
