using UnityEngine;
using System.Collections;

public class EnemyCollisions : MonoBehaviour
{

		public bool recoil;
		public int HealthPoints;
		// Use this for initialization
		void Start ()
		{
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
						GameObject.Find ("Enemy").GetComponent<SpriteRenderer> ().enabled = false;
						GameObject.Find ("EventListener").SendMessage ("NextScene", 3);
				}

		}
		void OnTriggerEnter2D (Collider2D collider)
		{
				if (collider.gameObject.tag == "Punch") {
						recoil = true;
						HealthPoints -= 10;
				}
		}
	
		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.collider.gameObject.tag == "Punch") {
				}
		}
}

