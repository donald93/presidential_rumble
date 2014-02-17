using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("this is a test");
	}

	
	void onTriggerEnter(Collider2D collider){
		
	}

	void OnCollisionEnter2D (Collision2D collision){
		if (collision.gameObject.tag == "Player")
			Destroy (this.gameObject);
	}
}
