using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour {
	/**
	 * Level Selector makes it so that a canvas displaying level details pops up on the level select scene 
	*/


	//Setting up swappable menus for each part.
	private Canvas levelPanel;
	private SelectionCust selection;
	/*public Canvas UpperArm;
	public Canvas LowerArm;
	public Canvas Hand;*/

	//Creating a boolean to prevent menu from closing when player clicks a level load button
	private bool canCloseMenu = true;

	//Setting up changeable States.
	private enum State {UpperArm, Forearm, Hand, Inst};
	private State bSection;

	//Access the Player Manager
	private PlayerManager pm;

	// Use this for initialization
	void Start () {

		selection = GetComponent <SelectionCust> ();
		levelPanel = GetComponentInChildren <Canvas> ();

		pm = FindObjectOfType <PlayerManager> ();

		//Make sure all level panels are turned off at the start.
		levelPanel.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		//Disable the level select if the player deSelects the button.
		if (selection.selected == false) {
			levelPanel.gameObject.SetActive (false);
		}
	
	}

	//Identify the section of the body part.
	/*void findSection ()
	{
		if (this.name == "Upper Arm") {
			bSection = State.UpperArm;
			print (bSection);
		} else if (this.name == "Forearm") {
			bSection = State.Forearm;
			print (bSection);
		} else if (this.name == "Hand") {
			bSection = State.Hand;
			print (bSection);
		} /*else if (this.name == "Instructions Button") {
			bSection = State.Inst;
			print (bSection);
		}*/

	//}

	//Once the body part is found, activate the appropriate menu.
	public void stageSelect(){
		//findSection ();
		selection.selected = true;
		levelPanel.gameObject.SetActive (true);
		}

	//Place on each button's OnClick event
	//Loads a level.
	public void loadLevel (string levelName){
		SceneManager.LoadScene (levelName);
	}


	//Closes the panel when the player deSelects the button.
	public void deSelect (){
		if (transform.gameObject.tag == "Panel") {
			selection.selected = true;
		} else {
			selection.selected = false;
		}
	}

	/*Place on the body part's Image's button's Event Trigger Component's DeSelect
	  Calls the deSelect () method.
	  Makes sure that the selected button has a chance to load the next scene before
	  Level select panel closes.*/
	public void delayDeSelect(){
		if (canCloseMenu) {
			Invoke ("deSelect", 0.15f);
		}
	}

	//Check to see if the user is trying to click a button to load the level.
	public void hoverOn(){
		canCloseMenu = false;
		print (canCloseMenu); 
	}

	public void hoverOff(){
		canCloseMenu = true;
		print (canCloseMenu); 
	}

	public void logout(){
		PlayerPrefs.DeleteKey ("Username");
		PlayerPrefs.DeleteKey ("Proficiency");
		PlayerPrefs.DeleteKey ("Displayname");
		PlayerPrefs.DeleteKey ("Optin");
		Destroy (pm.gameObject);
		loadLevel ("main");
	}


}
