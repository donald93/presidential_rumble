using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Called each update
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		rigidbody2D.velocity = movement;
	}
}
