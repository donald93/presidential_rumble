using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
		public bool jump, punch, goingLeft, crouch, recoil;
		public float jumpForce = 1000f;
		public int HealthPoints;
		public AudioClip jumpSound, punchHit;
	
		private bool grounded = false;
		private Transform groundCheck;
		private int framesSinceJump = 0;
		private GameObject GUI;
		protected Animator animator;

		void Start ()
		{
				Transform a = transform.Find ("Character animation");
				animator = a.GetComponent<Animator> ();
				HealthPoints = 100;
				GUI = GameObject.FindGameObjectWithTag ("GUI");
		}

		void Awake ()
		{
				groundCheck = transform.Find ("groundCheck");
		}

		void Update ()
		{
				grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

				if (animator) {

						//Crouching controls
						if (Input.GetKeyDown ("s")) {
				
								animator.SetBool ("Crouching", true);
								crouch = true;
						}

						if (Input.GetKeyUp ("s")) {
								animator.SetBool ("Crouching", false);
								crouch = false;
						} 



						// Jump Controls
						if (Input.GetKeyDown ("space") && grounded) {
								jump = true;
								animator.SetBool ("Jumping", true);
								framesSinceJump = 0;
								audio.PlayOneShot (jumpSound);
						}
			
						if (grounded && framesSinceJump > 0)
								animator.SetBool ("Jumping", false);



						// Punch key was pushed
						if (Input.GetKeyDown ("f")) {
								animator.SetBool ("Punching", true);
			
								// loop through children and enable the punch colliders
								Transform[] allChildren = GetComponentsInChildren<Transform> ();
								foreach (Transform child in allChildren) {
										if (child.tag == "Punch")
												child.collider2D.enabled = true;
										Invoke ("disablePunch", 0.1f);
								}
						} 

						if (Input.GetKeyDown ("v")) {
								animator.SetBool ("Kicking", true);
								// loop through children and enable the punch colliders
								Transform[] allChildren = GetComponentsInChildren<Transform> ();
								foreach (Transform child in allChildren) {
										if (child.tag == "Kick")
												child.collider2D.enabled = true;
										Invoke ("disableKick", 0.5f);
								}
						}
				}

				if (!goingLeft && Input.GetAxis ("Horizontal") < 0 || goingLeft && Input.GetAxis ("Horizontal") > 0) {
						Vector2 scale = transform.localScale;
						scale.x *= -1;
						transform.localScale = scale;
						goingLeft = !goingLeft;
				}
		}

		// Called each update
		void FixedUpdate ()
		{
				if (recoil) {
			
						//if (GameObject.Find ("Player").transform.position.x < this.transform.position.x)
						rigidbody2D.velocity -= new Vector2 (2500f, 0f);
						//else
						//		rigidbody2D.AddForce (new Vector2 (-2500f, 0f));
						recoil = false;	
						return;
			
				}
				float moveHorizontal = Input.GetAxis ("Horizontal");

				//Check if crouching to slow movement
				if (crouch && grounded) 
						rigidbody2D.velocity = new Vector2 (moveHorizontal * 15, rigidbody2D.velocity.y);
				else
						rigidbody2D.velocity = new Vector2 (moveHorizontal * 25, rigidbody2D.velocity.y);

				if (jump) {
						rigidbody2D.AddForce (new Vector2 (0f, jumpForce));	
						jump = false;
				}

				framesSinceJump++;		
		}

		void OnCollisionEnter2D (Collision2D collision)
		{

		}

		void OnTriggerEnter2D (Collider2D collider)
		{
				if (collider.gameObject.tag == "Punch") {
						recoil = true;
						HealthPoints -= 10;
						audio.PlayOneShot (punchHit);
				}

				GUI.SendMessage ("updatePlayerHealth", HealthPoints);
		}

		void disablePunch ()
		{
				animator.SetBool ("Punching", false);
				Transform[] allChildren = GetComponentsInChildren<Transform> ();
				foreach (Transform child in allChildren) {
						if (child.tag == "Punch")
								child.collider2D.enabled = false;
				}
		}

		void disableKick ()
		{
				animator.SetBool ("Kicking", false);
				Transform[] allChildren = GetComponentsInChildren<Transform> ();
				foreach (Transform child in allChildren) {
						if (child.tag == "Kick")
								child.collider2D.enabled = false;
				}
		}
}
