﻿using UnityEngine;
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
				Counter ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!Globals.paused) {
						grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

						if (!attacking && transform.position.x > 
								player.gameObject.GetComponent<PlayerController> ().getX () && transform.localScale.x > 0) {
								Vector2 scale = transform.localScale;
								scale.x *= -1;
								transform.localScale = scale;
						} else if (!attacking && transform.position.x < player.gameObject.GetComponent<PlayerController> ().getX () && transform.localScale.x < 0) {
								Vector2 scale = transform.localScale;
								scale.x *= -1;
								transform.localScale = scale;
						}
				}

		}

		void FixedUpdate ()
		{
				if (!Globals.paused) {
						animator.speed = 1;
						GetComponent<Rigidbody2D>().gravityScale = 15;
						
						if (mobile >= aggressive && mobile >= defensive) {
							
								if (Mathf.Abs (GetComponent<Rigidbody2D>().transform.position.x - player.transform.position.x) < 3) {
										moveHorizontal = 0;
										GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, GetComponent<Rigidbody2D>().velocity.y);
			
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
										GetComponent<Rigidbody2D>().AddForce (new Vector2 (0f, jumpForce));	
										jump = false;
										jumping = false;
								} 	
						}
							//(aggressive >= mobile)
							else if (aggressive >= defensive && aggressive >= mobile) {
								//Get away from the player. 
								//Check which wall they are on and jump over player once in range
								
								if (GetComponent<Rigidbody2D>().transform.position.x < 25 && (player.transform.position.x - GetComponent<Rigidbody2D>().transform.position.x)<10) {// &&player.transform.position.x > rigidbody2D.transform.position.x)	
										if (!jump && !jumping){	
					                       		Invoke ("Jump", 0.5f);
					                       }
									//player.transform.position.x < rigidbody2D.transform.position.x
								} else if (GetComponent<Rigidbody2D>().transform.position.x > -15 && (player.transform.position.x - GetComponent<Rigidbody2D>().transform.position.x)<10) {	
										if (!jump && !jumping){
												Invoke ("Jump", 0.5f);
										}
								} else {			
									MoveAwayFromPlayer();
									if(cooldown == 0 && animator != null)
									{
										int rand = Random.Range(0, 20);
										if(rand == 4 || rand ==8)
										{
											MoveTowardsPlayer();
											attacking = true;
											punch = true;
											kick = true;
											Kick();
											Punch();
											Kick();
										}
										else
										{
											block = true;
											Block();
										}
						
									}
									
								}
								
								//MoveTowardsPlayer();
								//Punch();
								//Punch();
								//Block();
								//Crouch a lot more (crouch punch)
								//Block alot more
								//Counter();
							
						} else if (defensive >= mobile && defensive >= aggressive) {

								if (Mathf.Abs (GetComponent<Rigidbody2D>().transform.position.x - player.transform.position.x) < 3) {
										moveHorizontal = 0;
										GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, GetComponent<Rigidbody2D>().velocity.y);
			
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
										GetComponent<Rigidbody2D>().AddForce (new Vector2 (0f, jumpForce));	
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
				} else {
						animator.speed = 0;
						GetComponent<Rigidbody2D>().velocity = Vector2.zero;
						GetComponent<Rigidbody2D>().gravityScale = 0;
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
								child.GetComponent<Collider2D>().enabled = true;
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
								child.GetComponent<Collider2D>().enabled = true;
						Invoke ("disableKick", 0.5f);
				}

		}

		void MoveTowardsPlayer ()
		{
				if (cooldown == 0) {
						if (player.transform.position.x < GetComponent<Rigidbody2D>().transform.position.x) {
								moveHorizontal = -1;
								GetComponent<Rigidbody2D>().velocity = new Vector2 (moveHorizontal * 15, GetComponent<Rigidbody2D>().velocity.y);
				
						} else if (player.transform.position.x > GetComponent<Rigidbody2D>().transform.position.x) {
								moveHorizontal = 1;
								GetComponent<Rigidbody2D>().velocity = new Vector2 (moveHorizontal * 15, GetComponent<Rigidbody2D>().velocity.y);
						}
	
				} else {
						cooldown--;
						GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				}

				if (player.transform.position.y > GetComponent<Rigidbody2D>().transform.position.y + 10 && grounded && !jump) {
						int rand = Random.Range (0, 3);
						if (rand == 1 && !jump && !jumping)
								Invoke ("Jump", .05f);
				}
		}
		
		void MoveAwayFromPlayer ()
		{
				if (cooldown == 0) {
						if (player.transform.position.x < GetComponent<Rigidbody2D>().transform.position.x) {
								moveHorizontal = 1;
								GetComponent<Rigidbody2D>().velocity = new Vector2 (moveHorizontal * 15, GetComponent<Rigidbody2D>().velocity.y);
				
						} else if (player.transform.position.x > GetComponent<Rigidbody2D>().transform.position.x) {
								moveHorizontal = -1;
								GetComponent<Rigidbody2D>().velocity = new Vector2 (moveHorizontal * 15, GetComponent<Rigidbody2D>().velocity.y);
						}
			
				} else {
						cooldown--;
						GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				}
		
				if (player.transform.position.y > GetComponent<Rigidbody2D>().transform.position.y + 10 && grounded && !jump) {
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
								child.GetComponent<Collider2D>().enabled = false;
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
								child.GetComponent<Collider2D>().enabled = false;
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

		void Counter ()
		{
				resetTimer = new Timer (4000);
				resetTimer.Elapsed += new ElapsedEventHandler (OnTimedEvent);
				resetTimer.Enabled = true;
				resetTimer.Interval = 4000;
			
		}
		
		public void OnTimedEvent (object source, ElapsedEventArgs e)
		{
				//Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
				mobile = 0;
				aggressive = 0;
				defensive = 0;
				Counter ();
		}
}

