using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public bool jump;
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

			int k = Animator.StringToHash("Base Layer.Idle");
			int j = stateInfo.nameHash;
			if (stateInfo.nameHash == Animator.StringToHash("Base Layer.Idle")){
				if (Input.GetKey ("f"))
					animator.SetBool("Punching", true);
				else
					animator.SetBool("Punching", false);
			}
		}

		if (Input.GetKeyDown ("space") && grounded) {
			jump = true;		
		}
	}

	// Called each update
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");;
		rigidbody2D.velocity = new Vector2(moveHorizontal*50, rigidbody2D.velocity.y);

		if (jump) {
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));	
			jump = false;
		}
	}


	void onCollisionEnter2D(Collision2D collision){

	}
}