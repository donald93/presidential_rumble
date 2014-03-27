﻿using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
		public int playerHealth, enemyHealth, maxHealth;
		private GameObject playerBar, enemyBar;

		// Use this for initialization
		void Start ()
		{
				playerHealth = 250;
				enemyHealth = 250;
				maxHealth = 250;
				playerBar = GameObject.FindGameObjectWithTag ("PlayerBar");
				enemyBar = GameObject.FindGameObjectWithTag ("EnemyBar");
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{
				//GUI.Box (new Rect (188, 41, Screen.width / 4.40f * (playerHealth / maxHealth), 20), "George Washington");
		}

		void updatePlayerHealth (int health)
		{
				playerHealth = health;
				playerBar.transform.localScale = new Vector3 (7.25f * playerHealth / maxHealth, 1f);

				if (playerHealth <= 0)
						NextScene ();

		}

		void updateEnemyHealth (int health)
		{
				enemyHealth = health;
				enemyBar.transform.localScale = new Vector3 (7.25f * enemyHealth / maxHealth, 1f);
				if (enemyHealth <= 0)
						NextScene ();
		}

		void NextScene ()
		{
				Application.LoadLevel ("WashingtonMap");
		}
}

