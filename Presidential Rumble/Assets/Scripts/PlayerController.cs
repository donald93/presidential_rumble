using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public bool jump, punch, goingRight;
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
		Debug.Log ("this is a test");
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (animator) {

			//Get the current state
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

			if (stateInfo.nameHash == Animator.StringToHash("Base Layer.Idle")){
				// Punch key was pushed
				if (Input.GetKey ("f")){
					animator.SetBool("Punching", true);

					// loop through children and enable the punch colliders
					Transform[] allChildren = GetComponentsInChildren<Transform>();
					foreach (Transform child in allChildren){
						if (child.tag == "Punch")
							child.collider2D.enabled = true;
						Invoke ("disablePunch", 1);
					}
				}

				else
					animator.SetBool("Punching", false);
			}
		}

		if (Input.GetKeyDown ("space") && grounded) {
			jump = true;		
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
		rigidbody2D.velocity = new Vector2(moveHorizontal*25, rigidbody2D.velocity.y);

		if (jump) {
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));	
			jump = false;
		}
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