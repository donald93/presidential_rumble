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

	void Awake(){
		startingPos = transform.position.x;
		endPos = startingPos + unitsToMove;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
		//counter = 1;
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

	}

	void FixedUpdate(){
		//rigidbody2D.velocity = movement;
		//rigidbody2D.transform.position;
	}
}
