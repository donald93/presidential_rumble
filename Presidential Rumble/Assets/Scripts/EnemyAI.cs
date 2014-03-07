using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

		float startingPos;
		public int unitsToMove = 5;
		public int moveSpeed = 2;
		public float endPos;
		public int frame = 0;
		public int cooldown = 0;

		public bool punch = false;

		private Vector2 movement;

		public Transform player; 


		protected Animator animator;

		float moveHorizontal;

		void Awake ()
		{
				startingPos = transform.position.x;
				endPos = startingPos + unitsToMove;
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


		}

		void FixedUpdate ()
		{
				if (cooldown == 0) {
						if (Mathf.Abs (rigidbody2D.transform.position.x - player.transform.position.x) < 3) {
								moveHorizontal = 0;
								rigidbody2D.velocity = Vector2.zero;
						
								if (cooldown == 0 && animator != null)
										punch = true;
						} else if (player.transform.position.x < rigidbody2D.transform.position.x) {
								moveHorizontal = -1;
								rigidbody2D.velocity = new Vector2 (moveHorizontal * 15, rigidbody2D.velocity.y);

						} else if (player.transform.position.x > rigidbody2D.transform.position.x) {
								moveHorizontal = 1;
								rigidbody2D.velocity = new Vector2 (moveHorizontal * 15, rigidbody2D.velocity.y);
						}
				
						if (punch)
								PunchAttack ();
				} else {
						cooldown--;
						rigidbody2D.velocity = Vector2.zero;
				}
		}

		/**
		 * Moves towards the player and attacks
		 * 
		 * */
		void PunchAttack ()
		{
				if (frame == 0) {
						rigidbody2D.velocity = new Vector2 (-15, rigidbody2D.velocity.y);
				} else if (frame == 5) {
						rigidbody2D.velocity = Vector2.zero;	
						animator.SetBool ("Punching", true);
				} else if (frame == 20) {
						frame = -1;
						animator.SetBool ("Punching", false);
						punch = false;
						cooldown = 100;
				}
				
				frame++;
		}
}

