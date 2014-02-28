using UnityEngine;
using System.Collections;

public class EventListener : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void NextScene (int seconds)
		{

				//yield return WaitForSeconds (seconds);
				Application.LoadLevel ("WashingtonMap");
		}
}
