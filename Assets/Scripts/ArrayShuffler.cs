using UnityEngine;
using System.Collections;

//Attach this script to an Empty Game Object in the Scene.

public class ArrayShuffler : MonoBehaviour {
	public GameObject[] States; //Expandable Array, Fill this up with objects in the editor
	private int j = 1;
	private LevelManager1 levelmanager; //Setting up LevelManager script access

	// Use this for initialization
	void Start () {
		//The 'Fisher Yates Shuffle'
		for (int i = States.Length - 1; i > 0; i--) {
			int r = Random.Range(0,i);
			GameObject temp = States[i];
			States[i] = States[r];
			States[r] = temp;
		}
		//Makes the first State active for the Player to place
		States [0].SetActive (true);
		levelmanager = GameObject.FindObjectOfType<LevelManager1>();  //To add objects to the level manager script
		levelmanager.startObjCount (States.Length); //Number of states that player will have to complete

	}//End Start

	// Update is called once per frame
	void Update () {
		//Turned off the update function for now since we will be calling the nextState function
		//from the QuizPopup script
		/*
		if (j <= States.Length - 1) {
			if (Input.GetMouseButtonUp (0)) {
				States[j].SetActive(true);
				j++;
			}
		}*/
		}//End Update

	public void nextState(){
		if (j <= States.Length - 1) {
				States[j].SetActive(true);
				j++;
			}
		}//End nextState


}
