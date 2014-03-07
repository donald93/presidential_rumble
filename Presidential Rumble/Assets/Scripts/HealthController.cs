using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
		public int playerHealth, enemyHealth, maxHealth;

		// Use this for initialization
		void Start ()
		{
				playerHealth = 100;
				enemyHealth = 100;
				maxHealth = 100;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{
				GUI.Box (new Rect (188, 41, Screen.width / 4.40f * (playerHealth / maxHealth), 20), "George Washington");
		}

		void updatePlayerHealth (int health)
		{
				playerHealth = health; 
		}

		void updateEnemyHealth (int health)
		{
				enemyHealth = health;
		}
}

