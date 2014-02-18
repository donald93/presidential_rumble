using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	protected Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (animator) {			
			//Get the current state
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			
			if (stateInfo.nameHash == Animator.StringToHash("Base Layer.treeHit")){
				animator.SetBool("Hit", false);
			}
		}
	}	
	
	void onTriggerEnter2D(Collider2D collider){

	}

	void OnCollisionEnter2D (Collision2D collision){
		if (collision.collider.gameObject.tag == "Punch"){
			if (animator) {			
				//Get the current state
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

				if (stateInfo.nameHash == Animator.StringToHash("Base Layer.Idle")){
					animator.SetBool("Hit", true);
				}
			}
		}
	}
}
