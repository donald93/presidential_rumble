using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	//Attempt 1
	//public Vector2 speed = new Vector2(1.0f, 0f);
	//public Vector2 direction = new Vector2(-1.0f, 0.0f);

	//Attempt 2
	float startingPos;
	float endPos;
	public int unitsToMove = 5;
	public int moveSpeed = 2;
	bool moveRight = true;

	private Vector2 movement;

	public Transform player;
	//private Transform myTransform;

	float moveHorizontal;

	void Awake(){
		startingPos = transform.position.x;
		endPos = startingPos + unitsToMove;
		//myTransform = transform;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
		//counter = 1;

		//This code will make the ENEMY patrol part of the level.
		/*
		if (moveRight) {
			rigidbody2D.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
		}
		if (rigidbody2D.transform.position.x >= endPos) {
			moveRight = false;
		}
		if (!moveRight) {
			rigidbody2D.transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
		}
		if (rigidbody2D.transform.position.x <= startingPos) {
			moveRight = true;
		}
		*/
		//myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;

	}

	void FixedUpdate(){
		//rigidbody2D.velocity = movement;
		//rigidbody2D.transform.position;
		//Vector2 PlayerVector = new Vector2(Vector2.MoveTowards (rigidbody2D.transform.position, target.transform.position, 25).x, 0);
		if (Mathf.Abs(rigidbody2D.transform.position.x - player.transform.position.x) < 5)
		{
			moveHorizontal = 0;
			rigidbody2D.velocity = Vector2.zero;
			Debug.Log("too close");
		}
		else if (player.transform.position.x < rigidbody2D.transform.position.x) 
		{
			moveHorizontal = -1;
			rigidbody2D.velocity = new Vector2 (moveHorizontal * 25, rigidbody2D.velocity.y);

		}

		else if (player.transform.position.x  > rigidbody2D.transform.position.x)
		{
			moveHorizontal = 1;
			rigidbody2D.velocity = new Vector2 (moveHorizontal * 25, rigidbody2D.velocity.y);

		}

		//Check players position against enemy position
		//if less, move to the left and edit 



		//rigidbody2D.velocity = PlayerVector;
	}
}
