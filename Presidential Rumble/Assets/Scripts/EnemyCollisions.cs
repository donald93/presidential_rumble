﻿using UnityEngine;
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
						
						//if (GameObject.Find ("Player").transform.position.x < this.transform.position.x)
						rigidbody2D.velocity += new Vector2 (2500f, 0f);
						//else
						//		rigidbody2D.AddForce (new Vector2 (-2500f, 0f));
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
								recoil = true;
								audio.PlayOneShot (punchHit);
								HealthPoints -= 10;
								invincible = true;
								GUI.SendMessage ("updateEnemyHealth", HealthPoints);
								Invoke ("disableInvincible", .5f);

						} else if (collider.gameObject.tag == "Kick") {
								recoil = true;
								audio.PlayOneShot (punchHit);
								HealthPoints -= 15;
								invincible = true;
								GUI.SendMessage ("updateEnemyHealth", HealthPoints);
								Invoke ("disableInvincible", .5f);
						}
				}
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

