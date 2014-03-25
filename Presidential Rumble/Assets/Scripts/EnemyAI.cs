using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

		float startingPos;
		public int unitsToMove = 5, framesSinceJump = 0, jumpForce = 3500;
		public int moveSpeed = 2;
		public float endPos;
		public int frame = 0;
		public int cooldown = 0;
		public bool punch = false, kick = false, jumping = false, jump = false, block = false, 
				grounded = false, attacking = false;
		private Vector2 movement;
		public Transform player;
		protected Animator animator;
		float moveHorizontal;
		private Transform groundCheck;


		void Awake ()
		{
				startingPos = transform.position.x;
				endPos = startingPos + unitsToMove;
				groundCheck = transform.Find ("groundCheck");

		}

		// Use this for initialization
		void Start ()
		{
				player = GameObject.Find ("Player").transform;
				Transform a = transform.Find ("Character animation");

				if (a != null && a.GetComponent<Animator> () != null)
						animator = a.GetComponent<Animator> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!Globals.paused) {
						grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

						if (!attacking && transform.position.x > 
								GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().getX () && transform.localScale.x > 0) {
								Vector2 scale = transform.localScale;
								scale.x *= -1;
								transform.localScale = scale;
						} else if (!attacking && transform.position.x < GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().getX () && transform.localScale.x < 0) {
								Vector2 scale = transform.localScale;
								scale.x *= -1;
								transform.localScale = scale;
						}
				}

		}

		void FixedUpdate ()
		{
				if (!Globals.paused) {
						if (Mathf.Abs (rigidbody2D.transform.position.x - player.transform.position.x) < 3) {
								moveHorizontal = 0;
								rigidbody2D.velocity = Vector2.zero;
			
								if (cooldown == 0 && animator != null) {
										int rand = Random.Range (0, 2);
										if (rand == 1) {
												attacking = true;
												punch = true;
												Punch ();
										} else {
												attacking = true;
												kick = true;
												Kick ();
										}
								}
								
						} else {
								int rand = Random.Range (0, 2);
								if (rand == 1)
										MoveTowardsPlayer ();
								else
										Invoke ("MoveTowardsPlayer", 1f);
						}	
						if (jump) {
								rigidbody2D.AddForce (new Vector2 (0f, jumpForce));	
								jump = false;
								jumping = false;
						}
		
						framesSinceJump++;	

						if (grounded && (framesSinceJump > 0)) {
								animator.SetBool ("Jumping", false);
								jumping = false;
								jump = false;
						}
				}
		}


		void Punch ()
		{
				animator.SetBool ("Punching", true);
				punch = true;	
				// loop through children and enable the punch colliders
				Transform[] allChildren = GetComponentsInChildren<Transform> ();
				foreach (Transform child in allChildren) {
						if (child.tag == "Punch")
								child.collider2D.enabled = true;
						Invoke ("disablePunch", 0.2f);
				}
		}

		void Kick ()
		{
				kick = true;				
				animator.SetBool ("Kicking", true);
				// loop through children and enable the punch colliders
				Transform[] allChildren = GetComponentsInChildren<Transform> ();
				foreach (Transform child in allChildren) {
						if (child.tag == "Kick")
								child.collider2D.enabled = true;
						Invoke ("disableKick", 0.5f);
				}

		}

		void MoveTowardsPlayer ()
		{
				if (cooldown == 0) {
						if (player.transform.position.x < rigidbody2D.transform.position.x) {
								moveHorizontal = -1;
								rigidbody2D.velocity = new Vector2 (moveHorizontal * 15, rigidbody2D.velocity.y);
				
						} else if (player.transform.position.x > rigidbody2D.transform.position.x) {
								moveHorizontal = 1;
								rigidbody2D.velocity = new Vector2 (moveHorizontal * 15, rigidbody2D.velocity.y);
						}
	
				} else {
						cooldown--;
						rigidbody2D.velocity = Vector2.zero;
				}

				if (player.transform.position.y > rigidbody2D.transform.position.y + 10 && grounded && !jump) {
						int rand = Random.Range (0, 3);
						if (rand == 1)
								Invoke ("Jump", .05f);
				}
		}

		void Jump ()
		{
				jump = true;
				jumping = true;
				block = false;
				animator.SetBool ("Jumping", true);
				framesSinceJump = 0;
				//audio.PlayOneShot (jumpSound);
		}

		void disableKick ()
		{
				attacking = false;
				kick = false;		
				animator.SetBool ("Kicking", false);
				Transform[] allChildren = GetComponentsInChildren<Transform> ();
				foreach (Transform child in allChildren) {
						if (child.tag == "Kick")
								child.collider2D.enabled = false;
				}
		}
		void disablePunch ()
		{
				attacking = false;
				punch = false;		
				animator.SetBool ("Punching", false);
				Transform[] allChildren = GetComponentsInChildren<Transform> ();
				foreach (Transform child in allChildren) {
						if (child.tag == "Punch")
								child.collider2D.enabled = false;
				}
		}
}

