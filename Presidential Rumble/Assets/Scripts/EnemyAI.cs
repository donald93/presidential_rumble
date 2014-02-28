using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

		float startingPos;
		public int unitsToMove = 5;
		public int moveSpeed = 2;
		public float endPos;

		private Vector2 movement;

		public Transform player; 

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
		}
	
		// Update is called once per frame
		void Update ()
		{


		}

		void FixedUpdate ()
		{
				if (Mathf.Abs (rigidbody2D.transform.position.x - player.transform.position.x) < 5) {
						moveHorizontal = 0;
						rigidbody2D.velocity = Vector2.zero;
				} else if (player.transform.position.x < rigidbody2D.transform.position.x) {
						moveHorizontal = -1;
						rigidbody2D.velocity = new Vector2 (moveHorizontal * 15, rigidbody2D.velocity.y);

				} else if (player.transform.position.x > rigidbody2D.transform.position.x) {
						moveHorizontal = 1;
						rigidbody2D.velocity = new Vector2 (moveHorizontal * 15, rigidbody2D.velocity.y);

				}

		}
}
