using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public Transform target;
	public int moveSpeed;
	private Transform myTransform;

	void Awake(){
		myTransform = transform;
	}
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.position += myTransform.position * moveSpeed * Time.deltaTime;
	}
}
