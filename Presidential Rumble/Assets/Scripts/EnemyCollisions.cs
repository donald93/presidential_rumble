using UnityEngine;
using System.Collections;

public class EnemyCollisions : MonoBehaviour
{

		public bool recoil;
		public int HealthPoints;
		public AudioClip punchHit;

		private bool invincible;
		private GameObject GUI;

		// Use this for initialization
		void Start ()
		{
				GUI = GameObject.FindGameObjectWithTag ("GUI");
				recoil = false;
				HealthPoints = 100;
		}
	
		// Update is called once per frame
		void Update ()
		{

		}	

		void FixedUpdate ()
		{

				if (recoil) {
						recoil = false;					
				}

				if (HealthPoints <= 0) {
						if (GameObject.Find ("Enemy").GetComponent<SpriteRenderer> () != null)
								GameObject.Find ("Enemy").GetComponent<SpriteRenderer> ().enabled = false;
						GameObject.Find ("EventListener").SendMessage ("NextScene", 3);
				}

		}
		void OnTriggerEnter2D (Collider2D collider)
		{
				if (!invincible) {		
						if (collider.gameObject.tag == "Punch") {
								HealthPoints -= 10;

						} else if (collider.gameObject.tag == "Kick") {
								HealthPoints -= 15;

						}

						recoil = true;
						audio.PlayOneShot (punchHit);
						invincible = true;
						Invoke ("disableInvincible", .5f);
						GUI.SendMessage ("updateEnemyHealth", HealthPoints);
				}
				if (HealthPoints <= 0)
						Globals.paused = true;

		}
	
		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.collider.gameObject.tag == "Punch") {
				}
		}
		void disableInvincible ()
		{
				invincible = false;
		}
		
		public float getX ()
		{

				return transform.position.x;
		}
}

