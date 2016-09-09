using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartPosition : MonoBehaviour {
	//Setting up bools and variables
	private bool bodyPart;
	private bool placed = false;
	private int rng;
	private LevelManager levelmanager;
	private int xOffset = 0;
	private static bool taken1 = false;
	private static bool taken2 = false;
	private static bool taken3 = false;
	private static bool taken4 = false;
	private static bool taken5 = false;
	private static bool taken6 = false;
	private static bool taken7 = false;
	private static bool taken8 = false;
	private static bool taken9 = false;
	private static bool taken10 = false;
	private static bool taken11 = false;
	private static bool taken12 = false;
	private static bool taken13 = false;
	private static bool taken14 = false;
	private static bool taken15 = false;

	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene ().name == "Forearm1A" || SceneManager.GetActiveScene ().name == "Forearm1B" || SceneManager.GetActiveScene ().name == "Hand1A")
			xOffset = 276;
		else
			xOffset = 0;
		

		bodyPart = (this.tag == "unPlaced");
		levelmanager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!placed) {
			Invoke ("reRoll", 0.05f);
		}

		if (levelmanager.resetted) {
			emptySeats ();
		}
	
	}

	void reRoll (){
		if (bodyPart && !placed){
			rng = Random.Range (0, 15);
			if (rng >= 0 && rng < 1 && !taken1){
				this.transform.position = new Vector3 (180+xOffset, 108, -10);
				taken1 = true;
				placed = true;
			}
			if (rng >= 1 && rng < 2 && !taken2){
				this.transform.position = new Vector3 (-180+xOffset, 108, -10);
				taken2 = true;
				placed = true;
			}
			if (rng >= 2 && rng < 3 && !taken3){
				this.transform.position = new Vector3 (-180+xOffset, -115, -10);
				taken3 = true;
				placed = true;
			}
			if (rng >= 3 && rng < 4 && !taken4){
				this.transform.position = new Vector3 (180+xOffset, -115, -10);
				taken4 = true;
				placed = true;
			}
			if (rng >= 4 && rng < 5 && !taken5){
				this.transform.position = new Vector3 (0+xOffset, 0, -10);
				taken5 = true;
				placed = true;
			}
			if (rng >= 5 && rng < 6 && !taken6){
				this.transform.position = new Vector3 (100+xOffset, 60, -10);
				taken6 = true;
				placed = true;
			}
			if (rng >= 6 && rng < 7 && !taken7){
				this.transform.position = new Vector3 (-80+xOffset, 50, -10);
				taken7 = true;
				placed = true;
			}
			if (rng >= 7 && rng < 8 && !taken8){
				this.transform.position = new Vector3 (100+xOffset, -200, -10);
				taken8 = true;
				placed = true;
			}
			if (rng >= 8 && rng < 9 && !taken9){
				this.transform.position = new Vector3 (75+xOffset, -110, -10);
				taken9 = true;
				placed = true;
			}
			if (rng >= 9 && rng < 10 && !taken10){
				this.transform.position = new Vector3 (0+xOffset, -135, -10);
				taken10 = true;
				placed = true;
			}
			if (rng >= 10 && rng < 11 && !taken11){
				this.transform.position = new Vector3 (50+xOffset, -160, -10);
				taken11 = true;
				placed = true;
			}
			if (rng >= 11 && rng < 12 && !taken12){
				this.transform.position = new Vector3 (-100+xOffset, 140, -10);
				taken12 = true;
				placed = true;
			}
			if (rng >= 13 && rng < 14 && !taken13){
				this.transform.position = new Vector3 (140+xOffset, 140, -10);
				taken13 = true;
				placed = true;
			}
			if (rng >= 14 && rng < 15 && !taken14){
				this.transform.position = new Vector3 (-140+xOffset, -190, -10);
				taken14 = true;
				placed = true;
			}
			if (rng >= 15 && rng < 16 && !taken15){
				this.transform.position = new Vector3 (-120+xOffset, -200, -10);
				taken15 = true;
				placed = true;
			}
		}

	}

	void emptySeats (){

			taken1 = false;
			taken2 = false;
			taken3 = false;
			taken4 = false;
			taken5 = false;
			taken6 = false;
			taken7 = false;
			taken8 = false;
			taken9 = false;
			taken10 = false;
			taken11 = false;
			taken12 = false;
			taken13 = false;
			taken14 = false;
			taken15 = false;
			levelmanager.resetted = false;

	}
}