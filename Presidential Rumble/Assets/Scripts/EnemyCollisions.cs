using UnityEngine;
using System.Collections;

public class EnemyCollisions : MonoBehaviour
{

		public bool recoil;
		public AudioClip punchHit;

		private bool invincible;
		private GameObject GUI;

		// Use this for initialization
		void Start ()
		{
				GUI = GameObject.FindGameObjectWithTag ("GUI");
				recoil = false;
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


		}
		void OnTriggerEnter2D (Collider2D collider)
		{
				int healthPoints = 0;
				if (!invincible) {		
						if (collider.gameObject.tag == "Punch") {
								healthPoints = 10;

						} else if (collider.gameObject.tag == "Kick") {
								healthPoints = 15;

						}

						recoil = true;
						audio.PlayOneShot (punchHit);
						invincible = true;
						Invoke ("disableInvincible", .5f);
						GUI.SendMessage ("updateEnemyHealth", healthPoints);
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

