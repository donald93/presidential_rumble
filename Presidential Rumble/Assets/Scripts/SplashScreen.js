#pragma strict

function Start () {

}

function Awake(){

	Splash();
}

function Splash(){

	yield WaitForSeconds(3.0);
	Application.LoadLevel("MainMenu");

}

function Update () {

}