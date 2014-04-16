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
				
				playerBar.transform.position = new Vector3 (playerBar.transform.position.x + 0.0025f * health / 15, playerBar.transform.position.y, playerBar.transform.position.z);

				if (playerHealth <= 0) {
						Globals.paused = true;
						Globals.GameState = BattleStateEnum.LOSE;
				}
		}

		void updateEnemyHealth (int health)
		{
				enemyHealth -= health;
				enemyBar.transform.localScale = new Vector3 ((float)enemyHealth / maxHealth, 1f, 1f);
				enemyBar.transform.position = new Vector3 (enemyBar.transform.position.x - 0.0025f * health / 15, enemyBar.transform.position.y, enemyBar.transform.position.z);
				if (enemyHealth <= 0) {
						Globals.paused = true;
						if (GameObject.Find ("Enemy").GetComponent<SpriteRenderer> () != null)
								GameObject.Find ("Enemy").GetComponent<SpriteRenderer> ().enabled = false;

						Globals.GameState = BattleStateEnum.WIN;
				}
		}
}

