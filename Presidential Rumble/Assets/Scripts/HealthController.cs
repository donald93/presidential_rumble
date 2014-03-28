using UnityEngine;
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
		}

		void updatePlayerHealth (int health)
		{
				playerHealth -= health;
				playerBar.transform.localScale = new Vector3 ((float)playerHealth / maxHealth, 1f, 1f);
				if (playerHealth <= 0) {
						Globals.paused = true;
						Globals.GameState = BattleStateEnum.LOSE;
				}
		}

		void updateEnemyHealth (int health)
		{
				enemyHealth -= health;
				enemyBar.transform.localScale = new Vector3 ((float)enemyHealth / maxHealth, 1f, 1f);
				if (enemyHealth <= 0) {
						Globals.paused = true;
						if (GameObject.Find ("Enemy").GetComponent<SpriteRenderer> () != null)
								GameObject.Find ("Enemy").GetComponent<SpriteRenderer> ().enabled = false;

						Globals.GameState = BattleStateEnum.WIN;
				}
		}

		void NextScene ()
		{
				Application.LoadLevel ("WashingtonMap");
		}
}

