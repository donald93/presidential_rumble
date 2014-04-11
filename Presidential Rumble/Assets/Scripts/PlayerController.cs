using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
		public bool jump, jumping, attacking, goingLeft, crouch, block, invincible, jumpStraight;
		public float jumpForce = 5000f;
		public AudioClip jumpSound, punchHit;
		public Transform enemy;
		private bool grounded = false;
		private Transform groundCheck;
		private int framesSinceJump = 0, recoilFrames = 0;
		private GameObject GUI;

		protected Animator animator;

		void Start ()
		{
				Transform a = transform.Find ("Character animation");
				animator = a.GetComponent<Animator> ();
				GUI = GameObject.FindGameObjectWithTag ("GUI");
				enemy = GameObject.FindGameObjectWithTag ("Enemy").transform;
		}

		void Awake ()
		{
				groundCheck = transform.Find ("groundCheck");
				Application.targetFrameRate = 60;
		}

		void Update ()
		{
				
				if (!Globals.paused) {
						grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

						if (animator) {

								//Crouching controls
								if (Input.GetKeyDown ("s") || Input.GetKeyDown (KeyCode.Joystick1Button5)) {
				
										animator.SetBool ("Crouching", true);
										crouch = true;
										enemy.GetComponent<EnemyAI> ().updateDefensive ();
								}

								if (Input.GetKeyUp ("s") || Input.GetKeyUp (KeyCode.Joystick1Button5)) {
										animator.SetBool ("Crouching", false);
										crouch = false;
								} 

								//Blocking controls
								if (Input.GetKeyDown ("q") || Input.GetKeyDown (KeyCode.Joystick1Button4)) {
										block = true;					
										animator.SetBool ("Blocking", true);
										enemy.GetComponent<EnemyAI> ().updateDefensive ();
								}

								if (Input.GetKeyUp ("q") || Input.GetKeyDown (KeyCode.Joystick1Button4)) {
										block = false;
										animator.SetBool ("Blocking", false);
								}

								// Jump Controls
								if ((Input.GetKeyDown ("space") || Input.GetKeyDown (KeyCode.Joystick1Button0) || Input.GetKeyDown (KeyCode.Joystick1Button3)) && grounded && !block && !attacking) {
										jump = true;
										jumping = true;
										jumpStraight = false;
										animator.SetBool ("Jumping", true);
										framesSinceJump = 0;
										audio.PlayOneShot (jumpSound);
										enemy.GetComponent<EnemyAI> ().updateMobile ();
								}
			
								if (framesSinceJump > 0 && grounded) {
										animator.SetBool ("Jumping", false);
										jumping = false;
								}



								// Punch key was pushed
								if ((Input.GetKeyDown ("f") || Input.GetKeyDown (KeyCode.Joystick1Button1)) && !attacking) {
										animator.SetBool ("Punching", true);
										attacking = true;	
										enemy.GetComponent<EnemyAI> ().updateAggressive ();
										// loop through children and enable the punch colliders
										Transform[] allChildren = GetComponentsInChildren<Transform> ();
										foreach (Transform child in allChildren) {
												if (child.tag == "Punch")
														child.collider2D.enabled = true;
												Invoke ("disablePunch", 0.2f);
										}
								} 

								// Kick key was pushed
								if ((Input.GetKeyDown ("v") || Input.GetKeyDown (KeyCode.Joystick1Button2)) && !attacking) {
										animator.SetBool ("Kicking", true);
										attacking = true;
										enemy.GetComponent<EnemyAI> ().updateAggressive ();
										// loop through children and enable the punch colliders
										Transform[] allChildren = GetComponentsInChildren<Transform> ();
										foreach (Transform child in allChildren) {
												if (child.tag == "Kick")
														child.collider2D.enabled = true;
												Invoke ("disableKick", 0.4f);
										}
								}
						}

						if (!attacking && transform.position.x > 
								GameObject.FindWithTag ("Enemy").GetComponent<EnemyCollisions> ().getX () && transform.localScale.x > 0) {
								Vector2 scale = transform.localScale;
								scale.x *= -1;
								transform.localScale = scale;
						} else if (!attacking && transform.position.x < GameObject.FindWithTag ("Enemy").GetComponent<EnemyCollisions> ().getX () && transform.localScale.x < 0) {
								Vector2 scale = transform.localScale;
								scale.x *= -1;
								transform.localScale = scale;
						}

				}
		}

		// Called each update
		void FixedUpdate ()
		{
				if (!Globals.paused) {
						if (recoilFrames > 0) {
						
								recoilFrames--;	
								return;
			
						}

						float moveHorizontal = Input.GetAxis ("Horizontal");

						if (jumping) {
								if (jumpStraight) {
										rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
										if (moveHorizontal > 0 || moveHorizontal < 0)
												jumpStraight = false;
								} else if (!goingLeft) {
										if (moveHorizontal > 0) {

												rigidbody2D.velocity = new Vector2 (25, rigidbody2D.velocity.y);
												int rand = Random.Range (0, 20);
												if (rand == 1) {
														enemy.GetComponent<EnemyAI> ().updateMobile ();
												}
												rigidbody2D.velocity = new Vector2 (25 * moveHorizontal, rigidbody2D.velocity.y);
										} else
												rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x - 0.25f, rigidbody2D.velocity.y);
								} else {
										if (moveHorizontal < 0) {
												rigidbody2D.velocity = new Vector2 (-25, rigidbody2D.velocity.y);
												int rand = Random.Range (0, 20);
												if (rand == 1) {
														enemy.GetComponent<EnemyAI> ().updateMobile ();
												}
					
												rigidbody2D.velocity = new Vector2 (25 * moveHorizontal, rigidbody2D.velocity.y);
										} else
												rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x + 0.25f, rigidbody2D.velocity.y);
								}

						} else if (crouch || attacking && !jumping || block) 
								rigidbody2D.velocity = new Vector2 (moveHorizontal * 0, rigidbody2D.velocity.y);
						else {
								rigidbody2D.velocity = new Vector2 (moveHorizontal * 25, rigidbody2D.velocity.y);
								if (moveHorizontal > 0)
										goingLeft = false;
								else
										goingLeft = true;
						}

						if (jump) {
								if (moveHorizontal == 0)
										jumpStraight = true;
								rigidbody2D.AddForce (new Vector2 (0f, jumpForce));	
								jump = false;
						}

						framesSinceJump++;		
				}
		}

		void OnCollisionEnter2D (Collision2D collision)
		{

		}

		void OnTriggerEnter2D (Collider2D collider)
		{
				int healthPoints = 0;

				if (collider.gameObject.tag == "Punch" && !invincible) {
						flinch ();
						recoilFrames = 5;
						invincible = true;
						Invoke ("disableInvincible", 0.5f);
						if (block) {
								healthPoints = 1;
						} else
								healthPoints = 10;
						audio.PlayOneShot (punchHit);
				}

				if (collider.gameObject.tag == "Kick" && !invincible) {
						flinch ();
						recoilFrames = 5;
						invincible = true;
						Invoke ("disableInvincible", 0.5f);
						if (block) {
								healthPoints = 1;
						} else
								healthPoints = 15;
						audio.PlayOneShot (punchHit);
				}

			
				GUI.SendMessage ("updatePlayerHealth", healthPoints);
		}

		void disableInvincible ()
		{
				invincible = false;
		}

		void disablePunch ()
		{
				attacking = false;		
				animator.SetBool ("Punching", false);
				Transform[] allChildren = GetComponentsInChildren<Transform> ();
				foreach (Transform child in allChildren) {
						if (child.tag == "Punch")
								child.collider2D.enabled = false;
				}
		}

		void disableKick ()
		{
				attacking = false;		
				animator.SetBool ("Kicking", false);
				Transform[] allChildren = GetComponentsInChildren<Transform> ();
				foreach (Transform child in allChildren) {
						if (child.tag == "Kick")
								child.collider2D.enabled = false;
				}
		}

		public float getX ()
		{
				return transform.position.x;
		}

		void flinch ()
		{
				attacking = false;
				animator.SetBool ("Kicking", false);
				animator.SetBool ("Punching", false);

				Transform[] allChildren = GetComponentsInChildren<Transform> ();

				foreach (Transform child in allChildren) {
						if (child.tag == "Kick" || child.tag == "Punch")
								child.collider2D.enabled = false;
				}
		}
}
