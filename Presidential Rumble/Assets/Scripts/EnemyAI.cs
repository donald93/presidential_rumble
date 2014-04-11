using UnityEngine;
using System.Collections;
using System.Timers;

public class EnemyAI : MonoBehaviour
{
		public int hitBoundary = 0;
		public int mobile = 0, aggressive = 0, defensive = 0;
		Timer resetTimer;
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
				Counter();
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
						
						if (mobile >= aggressive && mobile >= defensive) {
							
								if (Mathf.Abs (rigidbody2D.transform.position.x - player.transform.position.x) < 3) {
										moveHorizontal = 0;
										rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
			
										if (cooldown == 0 && animator != null) {
												int rand = Random.Range (0, 3);
												if (rand == 1) {
														attacking = true;
														punch = true;
														Punch ();

												} else if (rand == 2) {
														attacking = true;
														kick = true;
														Kick ();
														
												} else {
														Block ();
														
												}
											//Counter();
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
						}
							//(aggressive >= mobile)
							else if (aggressive >= defensive && aggressive >= mobile) {
								//Get away from the player. 
								//Check which wall they are on and jump over player once in range
								if (rigidbody2D.transform.position.x < 23 && player.transform.position.x > rigidbody2D.transform.position.x) {// &&player.transform.position.x > rigidbody2D.transform.position.x)	
										if (!jump && !jumping)
												Invoke ("Jump", 0.5f);
										MoveAwayFromPlayer();
								} else if (rigidbody2D.transform.position.x > -17 && player.transform.position.x < rigidbody2D.transform.position.x) {	
										if (!jump && !jumping)
											Invoke ("Jump", 0.5f);
										MoveAwayFromPlayer();
								} else {			
										MoveAwayFromPlayer();
								}
								
								//Crouch a lot more (crouch punch)
								//Block alot more
								//Counter();
							
						} else if (defensive >= mobile && defensive >= aggressive) {

								if (Mathf.Abs (rigidbody2D.transform.position.x - player.transform.position.x) < 3) {
										moveHorizontal = 0;
										rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
			
										if (cooldown == 0 && animator != null) {
												int rand = Random.Range (0, 3);
												if (rand == 1) {
														attacking = true;
														punch = true;
														kick = true;
														Punch ();
														Kick ();
														Kick ();
												} else if (rand == 2) {
														attacking = true;
														kick = true;
														punch = true;
														Kick ();
														Punch ();
														Punch ();
												} else if (rand == 0) {
														attacking = true;
														kick = true;
														punch = true;
														Kick ();
														Punch ();
														Kick ();

												}
											//Counter();
										}
								
								} else {
										int rand = Random.Range (0, 2);
										if (rand == 1)
												MoveTowardsPlayer ();
								}	
								if (jump) {
										rigidbody2D.AddForce (new Vector2 (0f, jumpForce));	
										jump = false;
										jumping = false;
								}
						}


						framesSinceJump++;	

						if (grounded && (framesSinceJump > 0)) {
								animator.SetBool ("Jumping", false);
								jumping = false;
								jump = false;
						}
				}
		}
		
		void Block ()
		{
				//animator.SetBool ("Block", true);
				block = true;


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
						if (rand == 1 && !jump && !jumping)
								Invoke ("Jump", .05f);
				}
		}
		
		void MoveAwayFromPlayer ()
		{
				if (cooldown == 0) {
						if (player.transform.position.x < rigidbody2D.transform.position.x) {
								moveHorizontal = 1;
								rigidbody2D.velocity = new Vector2 (moveHorizontal * 15, rigidbody2D.velocity.y);
				
						} else if (player.transform.position.x > rigidbody2D.transform.position.x) {
								moveHorizontal = -1;
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
		bool GetBlock ()
		{

				return block;
		}

		public void updateMobile ()
		{
				mobile++;
		}

		public void updateAggressive ()
		{
				aggressive++;
		}
		
		public void updateDefensive ()
		{
				defensive++;

		}

		public Animator getAnimator ()
		{
				return animator;
		}

		void Counter()
		{
			resetTimer = new Timer (5000);
			resetTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			resetTimer.Enabled = true;
			resetTimer.Interval = 5000;
			
		}
		
		public void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			//Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
			mobile = 0;
			aggressive = 0;
			defensive = 0;
			Counter();
		}
}

