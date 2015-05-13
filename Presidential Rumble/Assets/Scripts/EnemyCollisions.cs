using UnityEngine;
using System.Collections;

public class EnemyCollisions : MonoBehaviour
{

		public bool recoil;
		public AudioClip punchHit;

		private bool invincible;
		private GameObject GUI;
		private EnemyAI AI;

		// Use this for initialization
		void Start ()
		{
				AI = GetComponent<EnemyAI> ();
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
						GetComponent<AudioSource>().PlayOneShot (punchHit);
						invincible = true;
						Invoke ("disableInvincible", .5f);
						GUI.SendMessage ("updateEnemyHealth", healthPoints);
						flinch ();
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

		void flinch ()
		{
				AI.attacking = false;
				AI.kick = false;
				AI.kick = false;
				AI.getAnimator ().SetBool ("Kicking", false);
				AI.getAnimator ().SetBool ("Punching", false);

				Transform[] allChildren = GetComponentsInChildren<Transform> ();

				foreach (Transform child in allChildren) {
						if (child.tag == "Kick" || child.tag == "Punch")
								child.GetComponent<Collider2D>().enabled = false;
				}
		}
}

