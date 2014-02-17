using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public bool jump, stopHorizontal;
	public float jumpForce = 1000f;

	private bool grounded = false;
	private Transform groundCheck;

	protected Animator animator;

	void Start(){
				animator = GetComponent<Animator> ();
	}

	void Awake(){
		groundCheck = transform.Find ("groundCheck");
	}

	void Update(){
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (animator) {
			//Get the current state
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		}

		if (Input.GetKeyUp ("a") || Input.GetKeyUp ("d"))
			stopHorizontal = true;

		if (Input.GetKeyDown ("space") && grounded) {
			jump = true;		
		}
	}

	// Called each update
	void FixedUpdate(){
		float moveHorizontal;

		if (stopHorizontal) {
			moveHorizontal = 0f;
			stopHorizontal = false;
		}

		else		
			moveHorizontal = Input.GetAxis ("Horizontal");

		Vector2 movement = new Vector2(moveHorizontal*50, rigidbody2D.velocity.y);
		rigidbody2D.velocity = movement;

		if (jump) {
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));	
			jump = false;
		}
	}

	void onCollisionEnter2D(Collision2D collision){

	}
}