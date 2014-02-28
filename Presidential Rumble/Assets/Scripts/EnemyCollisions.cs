using UnityEngine;
using System.Collections;

public class EnemyCollisions : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}	
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag == "Punch"){
			Debug.Log("ouch, I've been punched");
		}
	}
	
	void OnCollisionEnter2D (Collision2D collision){
		if (collision.collider.gameObject.tag == "Punch"){
			Debug.Log("ouch, I've been punched");
		}
	}
}

