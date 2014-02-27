using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public bool jump, punch, goingRight, crouch;
	public float jumpForce = 1000f;
	
	private bool grounded = false;
	private Transform groundCheck;
	private int framesSinceJump = 0;
	protected Animator animator;

	void Start(){
		Transform a = transform.Find ("Character animation");
		animator = a.GetComponent<Animator> ();
	}

	void Awake(){
		groundCheck = transform.Find ("groundCheck");
	}

	void Update(){
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (animator) {

			//Crouching controls
			if (Input.GetKeyDown ("s")) {
				
				animator.SetBool ("Crouching", true);
				crouch = true;
			}
			if (Input.GetKeyUp ("s")) {
				animator.SetBool ("Crouching", false);
				crouch = false;
			} 

			if (grounded && framesSinceJump > 0)
				animator.SetBool("Jumping", false);

			//Get the current state
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

			if (stateInfo.nameHash == Animator.StringToHash("Base Layer.Idle") || stateInfo.nameHash == Animator.StringToHash("Base Layer.Empty State")){
				// Punch key was pushed
				if (Input.GetKey ("f")){
					//animator.SetBool("Punching", true);

					// loop through children and enable the punch colliders
					Transform[] allChildren = GetComponentsInChildren<Transform>();
					foreach (Transform child in allChildren){
						if (child.tag == "Punch")
							child.collider2D.enabled = true;
						Invoke ("disablePunch", 1);
					}
				}

				//else
					//animator.SetBool("Punching", false);

				if (Input.GetKeyDown ("space") && grounded) {
					jump = true;
					animator.SetBool("Jumping", true);
					framesSinceJump = 0;
				}
			}
		}

		if (!goingRight && Input.GetAxis ("Horizontal") < 0 || goingRight && Input.GetAxis ("Horizontal") > 0){
			Vector2 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
			goingRight = !goingRight;
		}
	}

	// Called each update
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Check if crouching to slow movement
		if (crouch) {

						rigidbody2D.velocity = new Vector2 (moveHorizontal * 15, rigidbody2D.velocity.y);
				}
		else
			rigidbody2D.velocity = new Vector2 (moveHorizontal * 25, rigidbody2D.velocity.y);

		if (jump) {
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));	
			jump = false;
		}

		framesSinceJump++;		
	}

	void onCollisionEnter2D(Collision2D collision){

	}

	void onTriggerEnter2D(Collider2D collider){

	}

	void disablePunch(){
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren){
			if (child.tag == "Punch")
				child.collider2D.enabled = false;
		}
	}
}